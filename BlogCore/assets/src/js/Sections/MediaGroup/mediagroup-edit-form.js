if (document.getElementById('mediagroup-edit-form') !== null) {
    new Vue({
        el: "#mediagroup-edit-form",
        data: function () {
            return {
                errors: {
                    name: null,
                    result: null
                },
                id: '',
                name: '',
                normalizedName: '',
                invalidRegex: /[^a-zA-Z0-9]/g,
                nameRegex: /^[A-Za-z0-9_]*$/,
                url: '/api/MediaGroup'
            };
        },
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
            this.name = this.$el.querySelector('#Name').value;
            this.normalizedName = this.$el.querySelector('#NormalizedName').value;
        },
        methods: {
            submit() {
                if (this.validateName()) {
                    common.put(`${this.url}(${this.id})`, { Id: this.id, Name: this.name, NormalizedName: this.normalizedName })
                        .then(() => window.location.href = "/Mediagroup/Index")
                        .catch(response => { this.errors.result = response.error.message; });
                }
            },
            normalize() {
                this.normalizedName = this.name.toLowerCase().replace(this.invalidRegex, '');
            },
            validateName() {
                if (this.name.trim() === '') {
                    this.errors.name = 'Name is required.';
                    return false;
                }
                else {
                    this.errors.name = null;
                }
                return true;
            }
        },
        watch: {
            name: function () {
                this.validateName();
                this.normalize();
            }
        }
    });
}