if (document.getElementById('achievement-edit-form') !== null) {
    new Vue({
        el: 'achievement-edit-form',
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
            this.title = this.$el.querySelector('#Title').value;
            this.type = this.$el.querySelector('#Type').value;
            this.year = this.$el.querySelector('#Year').value;
        },
        data: function () {
            return {
                errors: {
                    result: null,
                    title: null,
                    type: null,
                    year: null
                },
                id: '',
                title: '',
                type: '',
                url: '/api/achievement',
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
                    common.put(`${this.url}(${this.id})`, { Title: this.title, Type: this.type, Year: this.year })
                        .then(() => window.location.href = "/Achievement/Index")
                        .catch(response => console.error(response.error.message));
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