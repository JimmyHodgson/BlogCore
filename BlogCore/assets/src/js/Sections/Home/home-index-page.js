if (document.getElementById('home-index-page') !== null) {
    new Vue({
        el: "#home-index-page",
        beforeMount() {
            common.get(this.endpoint)
                .then(response => this.source = response.value)
                .catch(error => console.error(error));

            this.profile = this.$el.querySelector('#Link').value;
        },
        components: {
            galleryPickerComponent
        },
        data: function () {
            return {
                contactImage:'',
                endpoint: '/api/MediaLink?$expand=Group',
                landingImage: '',
                skillsImage: '',
                source:[],
                profile: ''
            };
        },
        methods: {
            getProfilePic() {
                return this.profile || '/assets/dist/images/default_profile_pic.png';
            }
        }
    });
}