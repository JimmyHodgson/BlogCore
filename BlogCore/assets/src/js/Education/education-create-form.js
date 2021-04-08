if (document.getElementById('education-create-form') !== null) {
    new Vue({
        el: '#education-create-form',
        beforeMount() {
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
                    result:null,
                    school: null,
                    title: null,
                    year:null
                },
                imageUrl: '',
                link: '',
                school: '',
                source: [],
                title:'',
                url: '',
                year:'2000'
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
                    console.log('trigger');
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
                if (this.link.length > 0) {
                    this.errors.link = null;
                    return true;
                }
                this.errors.link = "Link is required.";
                return false;
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
        }
    });
}