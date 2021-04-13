if (document.getElementById('skill-create-form') !== null) {
    new Vue({
        el: '#skill-create-form',
        data: function () {
            return {
                errors: {
                    name: null,
                    proficiency:null,
                    result: null
                },
                name: '',
                proficiency:'Basic',
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
                common.post(this.url, { Name: this.name, Proficiency: this.proficiency })
                    .then(() => window.location.href = "Index")
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
            proficiency:'validateProficiency'
        }
    });
}