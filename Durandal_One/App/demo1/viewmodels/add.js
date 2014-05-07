define(['durandal/app', 'knockout', 'plugins/router', 'ko-validation'], function (app, ko, router, validation) {

    function Person(item) {
        this.Name = ko.observable(item.Name);
        this.Age = ko.observable(item.Age);
    }

    function ViewModel() {
        var self = this;

        self.inputName = ko.observable('').extend({
            required: true,
            minLength: 3
        });
        self.inputAge = ko.observable('');


        self.addPerson = function () {
            var newPerson = new Person({ Name: self.inputName(), Age: self.inputAge() });
            $.ajax("/api/testapi/PostPerson", {
                data: ko.toJSON(newPerson),
                type: "POST",
                contentType: "application/json"
            }).then(function() {
                router.navigate('');
            });
        };
    }

    return ViewModel;
});