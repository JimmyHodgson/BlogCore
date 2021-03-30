if (document.getElementById('mediagroup-create-form') !== null) {
    new Vue({
        el: "#mediagroup-create-form",
        components: {
            confirmationComponent
        },
        data: function () {
            return {
                errors: {
                    name:null
                },
                invalidRegex: /[^a-zA-Z0-9]/g,
                name: '',
                nameRegex: /^[A-Za-z0-9_]*$/,
                normalizedName:'',
                url: '/api/MediaGroup',
                visible: false,
                modal: {
                    header: 'Are you sure?',
                    body: 'This action cannot be reversed.'
                }
            };
        },
        methods: {
            submit() {
                if (this.validateName()) {
                    //ToDo
                    //common.post(this.url, { Name: this.name, NormalizedName: this.normalizedName })
                    //    .then(response => response.json().then(data=>console.log(data)))
                    //    .catch(error => console.error(error));

                    this.visible = true;
                }
            },
            confirm() {
                console.log("confirmed!");
                this.visible = false;
            },
            cancel() {
                console.log('cancelled!');
                this.visible = false;
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