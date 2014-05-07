define(['durandal/app', 'knockout'], function (app, ko) {
    
    function Person(item) {
        this.PersonId = ko.observable(item.PersonId);
        this.Name = ko.observable(item.Name);
        this.Age = ko.observable(item.Age);
    }
    
    function ViewModel() {
        var self = this;
        
        self.people = ko.observableArray([]);
        
        self.activate = function () {
            return $.getJSON('/api/testapi/GetAllPersons').done(function (data) {
                self.people($.map(data, function (item) {
                    return new Person(item);
                }));
            });
        };

        self.select = function(item) {
            item.viewUrl = 'demo1/views/details';
            app.showDialog(item);
        };
 
        self.removePerson = function (p) {
            var yes = 'Yes';
            var no = 'No';

            app.showMessage('Are you sure you want to remove ' + p.Name() + ' ?', 'Remove person', [yes, no]).then(function (resp) {
                if (resp == yes) {
                    self.people.remove(p);
                    $.ajax({
                        type: 'DELETE',
                        url: '/api/testapi/DeletePerson?id=' + p.PersonId()
                    });
                }
            });
        };
    }
 
    return ViewModel;
});