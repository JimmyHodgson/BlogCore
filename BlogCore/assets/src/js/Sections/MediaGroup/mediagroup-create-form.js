if (document.getElementById('mediagroup-create-form') !== null) {
    Promise.all([
        import('vue'),
        import('components'),
        import('common')
    ]).then(([{ createApp }, { confirmationComponent }, { common }]) => {
        createApp({
            components: {
                confirmationComponent
            },
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
                        common.post(this.url, { Name: this.name, NormalizedName: this.normalizedName })
                            .then(() => window.location.href = "Index")
                            .catch(response => { console.log(response); this.errors.result = response.error.message; });
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
        }).mount("#mediagroup-create-form");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}
