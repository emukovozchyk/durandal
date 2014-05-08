define(['durandal/app', 'knockout', 'toastr'], function (app, ko, toastr) {
    
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "positionClass": "toast-bottom-right",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };
    
    function Person(item) {
        this.Id = ko.observable(item.Id);
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
                        url: '/api/testapi/DeletePerson?id=' + p.Id(),
                        success: function () {
                            toastr.success('Deleted!', 'Delete person');
                        },
                        error: function () {
                            toastr.error('Something go wrong!', 'Delete person');
                        }
                    });
                }
            });
        };
    }
 
    return ViewModel;
});