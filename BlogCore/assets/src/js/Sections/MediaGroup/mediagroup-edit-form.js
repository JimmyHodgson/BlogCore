if (document.getElementById('mediagroup-edit-form') !== null) {
    Promise.all([
        import('vue'),
        import('common')
    ]).then(([{ createApp }, { common }]) => {
        createApp({
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
            mounted() {
                this.id = document.getElementById('Id')._value;
                this.name = document.getElementById('Name')._value;
                this.normalizedName = document.getElementById('NormalizedName')._value;
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
        }).mount("#mediagroup-edit-form");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}
