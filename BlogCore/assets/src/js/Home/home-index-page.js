if (document.getElementById('home-index-page') !== null) {
    new Vue({
        el: "#home-index-page",
        components: {
            galleryPickerComponent
        },
        data: function () {
            return {
                endpoint:'/api/MediaLink?$expand=Group',
                imageUrl: ''
            };
        }
    });
}