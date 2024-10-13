if (document.getElementById('achievement-edit-form') !== null) {
    Promise.all([
        import('vue'),
        import('common')
    ]).then(([{ createApp }, { common }]) => {
        const _Type = document.getElementById('Type').value;
        createApp({
            mounted() {
                this.id = document.getElementById('Id')._value;
                this.title = document.getElementById('Title')._value;
                this.type = _Type;
                this.year = document.getElementById('Year')._value;
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
                        common.put(`${this.url}(${this.id})`, { Id: this.id, Title: this.title, Type: this.type, Year: this.year })
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
                    if (parseInt(this.year)) {
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
        }).mount('#achievement-edit-form');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}
