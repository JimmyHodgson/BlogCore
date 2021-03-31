if (document.getElementById('mediagroup-edit-form') !== null) {
    new Vue({
        el: "#mediagroup-edit-form",
        data: function () {
            return {
                errors: {
                    name: null,
                    result: null
                },
                invalidRegex: /[^a-zA-Z0-9]/g,
                name: '',
                nameRegex: /^[A-Za-z0-9_]*$/,
                normalizedName: '',
                url: '/api/MediaGroup'
            };
        },
        methods: {
            submit() {
                if (this.validateName()) {
                    common.put(this.url, { Name: this.name, NormalizedName: this.normalizedName })
                        .then(() => window.location.href = "Index")
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