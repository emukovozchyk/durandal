define(['plugins/router', 'knockout', 'toastr'], function (router, ko, toastr) {
    
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

    function ViewModel() {
        var self = this;
            
        self.editName = ko.observable('');
        self.editAge = ko.observable('');
        self.bufferedItem = ko.observable('');

        self.activate = function (passedId) {
            return $.getJSON('/api/testapi/GetPerson/' + passedId).done(function (data) {
                self.bufferedItem = data;
                self.editName(self.bufferedItem.Name);
                self.editAge(self.bufferedItem.Age);
            });
        };
        
        self.editPerson = function () {
            self.bufferedItem.Name = self.editName();
            self.bufferedItem.Age = self.editAge();
            $.ajax("/api/testapi/PutPerson?id=" + self.bufferedItem.Id, {
                data: ko.toJSON(self.bufferedItem),
                type: "put",
                contentType: "application/json",
                success: function () {
                    toastr.success('Saved!', 'Edit person');
                },
                error: function () {
                    toastr.error('Something go wrong!', 'Edit person');
                }
            }).then(function() {
                router.navigate('');
            });
        };
    }
    return ViewModel;
});