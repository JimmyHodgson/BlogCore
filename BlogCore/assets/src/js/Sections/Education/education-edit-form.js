if (document.getElementById('education-edit-form') !== null) {
    new Vue({
        el: '#education-edit-form',
        beforeMount() {
            this.id = this.$el.querySelector('#Id').value;
            this.imageUrl = this.$el.querySelector('#ImageUrl').value;
            this.link = this.$el.querySelector('#Link').value;
            this.school = this.$el.querySelector('#School').value;
            this.title = this.$el.querySelector('#Title').value;
            this.year = this.$el.querySelector('#Year').value;

            common.get(this.endpoint)
                .then(response => this.source = response.value)
                .catch(error => console.error(error));
        },
        components: {
            galleryPickerComponent
        },
        data: function () {
            return {

                endpoint: '/api/MediaLink?$expand=Group',
                errors: {
                    imageUrl: null,
                    link: null,
                    result: null,
                    school: null,
                    title: null,
                    year: null
                },
                id:'',
                imageUrl: '',
                link: '',
                school: '',
                source: [],
                title: '',
                url: '/api/Education',
                urlRegex: /[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)/,
                year: ''
            };
        },
        methods: {
            checkForm() {
                const imageUrl = this.validateImageUrl();
                const link = this.validateLink();
                const school = this.validateSchool();
                const title = this.validateTitle();
                const year = this.validateYear();
                return imageUrl && link && school && title && year;
            },
            submit() {
                if (this.checkForm()) {
                    common.put(`${this.url}(${this.id})`, { Id: this.id, School: this.school, Link: this.link, Title: this.title, ImageUrl: this.imageUrl, Year: this.year })
                        .then(() => window.location.href = "/Education/Index")
                        .catch(response => this.errors.result = response.error.message);
                }
            },
            validateImageUrl() {
                if (this.imageUrl.length > 0) {
                    this.errors.imageUrl = null;
                    return true;
                }
                this.errors.imageUrl = "Image Url is required.";
                return false;
            },
            validateLink() {
                if (this.link.length === 0) {
                    this.errors.link = "Link is required.";
                    return false;
                }
                if (!this.link.match(this.urlRegex)) {
                    this.errors.link = "Link must be a website url";
                    return false;
                }
                this.errors.link = null;
                return true;
            },
            validateSchool() {
                if (this.school.length > 0) {
                    this.errors.school = null;
                    return true;
                }
                this.errors.school = "School is required.";
                return false;
            },
            validateTitle() {
                if (this.title.length > 0) {
                    this.errors.title = null;
                    return true;
                }
                this.errors.title = "Title is required";
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
            imageUrl: 'validateImageUrl',
            link: 'validateLink',
            school: 'validateSchool',
            title: 'validateTitle',
            year: 'validateYear'
        }
    });
}