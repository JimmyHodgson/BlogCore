if (document.getElementById('registration-form') !== null) {
    new Vue({
        el: '#registration-form',
        data: function () {
            return {
                errors: ['', '', '', '', ''],
                firstName: '',
                lastName: '',
                email: '',
                password: '',
                confirm: ''

            };
        },
        watch: {
            firstName: function (newVal) {
                this.errors[0] = this.validateEmpty('First name', newVal);
            },
            lastName: function (newVal) {
                this.errors[1] = this.validateEmpty('Last name', newVal);
            },
            email: function (newVal) {
                this.errors[2] = this.validateEmail(newVal);
            },
            password: function (newVal) {
                this.errors[3] = this.validatePassword(newVal);
                this.errors[4] = this.validateConfirm();
            },
            confirm: function (newVal) {
                this.errors[4] = this.validateConfirm(newVal);
            }
        },
        methods: {
            checkForm: function (e) {
                return this.errors.join('').length > 0 ? e.preventDefault() : true;
            },
            validateEmpty(field, value) {
                let message = '';
                if (value.length === 0) {
                    message = `${field} is required.`;
                }
                return message;
            },
            validateEmail(value) {
                const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                let result = re.test(String(value).toLowerCase());
                let message = '';
                if (!result) {
                    message = 'Please enter a valid email.';
                }
                return message;
            },
            validatePassword(value) {
                let message = '';
                if (value.length < 6) {
                    message = 'Password must be at least 6 characters.';
                }
                return message;
            },
            validateConfirm(value = this.confirm) {
                let message = '';
                if (value !== this.password) {
                    message = "The password doesn't match.";
                }
                return message;
            }
        }
    });
}
