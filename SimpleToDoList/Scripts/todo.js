var Task = function (parent, title, finished) {
    var self = this;

    self.title = ko.observable(title);
    self.finished = ko.observable(finished);

    self.finished.subscribe(function() {
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

    self.sendCreate = function () {
        //alert(self.addItemTitle());
        $.ajax({
            url: "/api/todo",
            data: ko.toJSON({ 'Title': self.addItemTitle(), 'Finished': false }),
            type: 'POST',
            contentType: "application/json",
            statusCode: {
                201: // code for created, which is set in the controller api method
                    function (data) {
                        //alert(data);
                        self.add(data.Title, data.Finished);
                    }
            }
        });
        self.addItemTitle("");
    };

    self.sendDelete = function(task) {
        $.ajax({
            url: "/api/todo",
            type: "DELETE",
            contentType: "application/json",
            data: ko.toJSON({ 'Title': task.title() }),
            success: function() {
                self.tasks.remove(task);
            }
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
    }
};

$(function () {

    var viewModel = new MyViewModel();

    ko.applyBindings(viewModel);

    $.get("api/todo", function (items) {
        $.each(items, function (idx, item) {
            //alert(item.Title);
            viewModel.add(item.Title, item.Finished);
        });
    }, "json");

});
