if (document.getElementById('home-index-page') !== null) {
    new Vue({
        el: "#home-index-page",
        beforeMount() {
            common.get(this.endpoint)
                .then(response => this.imageSource = response.value)
                .catch(error => console.error(error));
        },
        components: {
            galleryPickerComponent
        },
        data: function () {
            return {
                endpoint: '/api/MediaLink?$expand=Group',
                imageSource:[],
                imageUrl: ''
            };
        }
    });
}