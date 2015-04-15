var Task = function (parent, title, finished) {
    var self = this;

    self.title = ko.observable(title);
    self.finished = ko.observable(finished);

    self.finished.subscribe(function () {
        parent.sendUpdate(self);
    });

    self.remove = function () {
        parent.sendDelete(self);
    };
};

var MyViewModel = function () {
    var self = this;


    //function Task(parent, title, finished) {
    //    var self = this;

    //    self.title = ko.observable(title);
    //    self.finished = ko.observable(finished);

    //    self.finished.subscribe(function () {
    //        parent.sendUpdate(self);
    //    });

    //    self.remove = function () {
    //        parent.tasks.remove(self);
    //    };
    //};


    self.tasks = ko.observableArray();
    //    [new Task(self, "First Task", true),new Task(self, "Second task", false),new Task(self, "Third task", false)]);

    self.addItemTitle = ko.observable("");

    self.add = function (title, finished) {
        self.tasks.push(new Task(self, title, finished));
    };

    self.removeById = function (titleParam) {
        //http://knockoutjs.com/documentation/observableArrays.html
        self.tasks.remove(
            function (item) { return item.title() == titleParam; }
        );
    };

    self.updateById = function (titleParam, finishedParam) {
        // to get the array element, see http://www.adamthings.com/post/2013/08/26/find-element-observable-array-knockoutjs/
        var match = ko.utils.arrayFirst(self.tasks(), function (item) {
            return titleParam === item.title();
        });
        if (match) {
            match.finished(finishedParam);
        }
    };

    self.sendCreate = function () {
        //alert(self.addItemTitle());
        $.ajax({
            url: "/api/todo",
            data: ko.toJSON({ 'Title': self.addItemTitle(), 'Finished': false }),
            type: 'POST',
            contentType: "application/json"

            // We dont need this any more, now we have SignalR - the hub calls hub.addItem (see below)
            //statusCode: {
            //    201: // code for created, which is set in the controller api method
            //        function (data) {
            //            //alert(data);
            //            self.add(data.Title, data.Finished);
            //        }
            //}
        });
        self.addItemTitle("");
    };

    self.sendDelete = function (task) {
        $.ajax({
            url: "/api/todo",
            type: "DELETE",
            contentType: "application/json",
            data: ko.toJSON({ 'Title': task.title() })

            // We dont need this any more, now we have SignalR
            //success: function() {
            //    self.tasks.remove(task);
            //}
        });
    };

    self.sendUpdate = function (task) {
        //alert(task.title());
        $.ajax({
            url: "/api/todo",
            type: "PUT",
            contentType: "application/json",
            data: ko.toJSON({ 'Title': task.title(), 'Finished': task.finished() })
        });
    };
};

$(function () {

    var viewModel = new MyViewModel();
    var hub = $.connection.todoHub; // Look for the SignalR hub called TodoHub

    ko.applyBindings(viewModel);
    
    // Functions which are called by the Signal TodoHub
    hub.client.addItem = function(item) {
        viewModel.add(item.Title, item.Finished);
    };
    hub.client.deleteItem = function (item) {
        viewModel.removeById(item.Title);
    };
    hub.client.updateItem = function (item) {
        viewModel.updateById(item.Title, item.Finished);
    };

    $.connection.hub.start();


    $.get("api/todo", function (items) {
        $.each(items, function (idx, item) {
            //alert(item.Title);
            viewModel.add(item.Title, item.Finished);
        });
    }, "json");

});
