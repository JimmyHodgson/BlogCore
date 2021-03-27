if (document.getElementById('sidebar-toggle-widget') !== null) {
    new Vue({
        el: "#sidebar-toggle-widget",
        data: function () {
            return {
                sidebar:true
            };
        },
        methods: {
            toggleSideBar() {
                this.sidebar = !this.sidebar;
                $("#side-bar").toggleClass('-closed', this.sidebar);
            }
        },
        mounted() {
            if (localStorage.blogcore_sidebar) {
                this.sidebar = localStorage.blogcore_sidebar === "true";
                $("#side-bar").toggleClass('-closed', this.sidebar);
            }
        },
        watch: {
            sidebar(newVal) {
                localStorage.blogcore_sidebar = newVal;
            }
        }
    });
}