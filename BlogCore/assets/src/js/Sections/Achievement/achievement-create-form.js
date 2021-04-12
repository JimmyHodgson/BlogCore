if (document.getElementById('achievement-create-form') !== null) {
    new Vue({
        el: '#achievement-create-form',
        data: function () {
            return {
                errors: {
                    result: null,
                    title: null,
                    type: null,
                    year: null
                },
                title: '',
                type: '',
                url: '/api/Achievment',
                year: ''
            };
        },
        methods: {
            checkForm() {
                const title = this.validateTitle();
                const type = this.validateType();
                const year = this.validateYear();
                return title && type && year;
            },
            submit() {
                if (this.checkForm()) {
                    common.post(this.url, { Title: this.title, Type: this.type, Year: this.year })
                        .then(() => window.location.href = "Index")
                        .catch(response => this.errors.result = response.error.message);
                }
            },
            validateTitle() {
                if (this.title.length > 0) {
                    this.errors.title = null;
                    return true;
                }
                this.errors.title = "Title is required";
                return false;
            },
            validateType() {
                if (this.type.length > 0) {
                    this.errors.type = null;
                    return true;
                }
                this.errors.type = "Type is required";
                return false;
            },
            validateYear() {
                if (this.year.length > 0) {
                    this.errors.year = null;
                    return true;
                }
                this.errors.year = "Year must be a number.";
                return false;
            }
        },
        watch: {
            title: 'validateTitle',
            type: 'validateType',
            year: 'validateYear'
        }
    });
}