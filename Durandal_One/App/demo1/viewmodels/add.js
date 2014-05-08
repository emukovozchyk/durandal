define(['durandal/app', 'knockout', 'plugins/router', 'knockout.validation', 'toastr'], function (app, ko, router, validation, toastr) {

    ko.validation.init({
        insertMessages: false
    });
    
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
        this.Name = ko.observable(item.Name);
        this.Age = ko.observable(item.Age);
    }

    var ViewModel = {        
        inputName: ko.observable('').extend({ required: true, minLength: 5, maxLength: 20 }),
        inputAge: ko.observable('').extend({ required: true, max: 100, min: 18 }),

        addPerson: function () {
            if (ViewModel.errors().length == 0) {
                var self = this;
                var newPerson = new Person({ Name: self.inputName(), Age: self.inputAge() });
                $.ajax("/api/testapi/PostPerson", {
                    data: ko.toJSON(newPerson),
                    type: "POST",
                    contentType: "application/json",
                    success: function() {
                        toastr.success('Added!', 'Add new person');
                    },
                    error: function() {
                        toastr.error('Something go wrong!', 'Add new person');
                    }
                }).then(function() {
                    router.navigate('');
                });
            } else {
                toastr.warning('Validation errors!', 'Add new person');
                ViewModel.errors.showAllMessages();
            }
        }
    };
    
    ViewModel.errors = ko.validation.group(ViewModel);
    
    return ViewModel;
});