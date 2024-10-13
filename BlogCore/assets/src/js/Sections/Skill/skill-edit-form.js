if (document.getElementById('skill-edit-form') !== null) {
    const _proficiency = document.getElementById('Proficiency').value;

    Promise.all([
        import('vue'),
        import('common')
    ]).then(([{ createApp }, { common }]) => {
        createApp({
            mounted() {
                this.id = document.getElementById('Id')._value;
                this.name = document.getElementById('Name')._value;
                this.proficiency = _proficiency
            },
            data: function () {
                return {
                    errors: {
                        name: null,
                        proficiency: null,
                        result: null
                    },
                    id: '',
                    name: '',
                    proficiency: 'Basic',
                    url: '/api/Skill'
                };
            },
            methods: {
                checkForm() {
                    const name = this.validateName();
                    const proficiency = this.validateProficiency();

                    return name && proficiency;
                },
                submit() {
                    common.put(`${this.url}(${this.id})`, { Id: this.id, Name: this.name, Proficiency: this.proficiency })
                        .then(() => window.location.href = "/Skill/Index")
                        .catch(response => this.errors.result = response.error.message);
                },
                validateName() {
                    if (this.name.length > 0) {
                        this.errors.name = null;
                        return true;
                    }
                    this.errors.name = 'Skill name is required.';
                    return false;
                },
                validateProficiency() {
                    if (this.proficiency.length > 0) {
                        this.errors.proficiency = null;
                        return true;
                    }
                    this.errors.proficiency = 'Proficiency level is required.';
                    return false;
                }
            },
            watch: {
                name: 'validateName',
                proficiency: 'validateProficiency'
            }
        }).mount('#skill-edit-form');
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}
