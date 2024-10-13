if (document.getElementById('sidebar-toggle-widget') !== null) {
    import("vue").then(({ createApp }) => {
        createApp({
            data: function () {
                return {
                    sidebar: true
                };
            },
            methods: {
                toggleSideBar() {
                    this.sidebar = !this.sidebar;
                    $("#side-bar").toggleClass('-closed', !this.sidebar);
                }
            },
            mounted() {
                if (localStorage.blogcore_sidebar) {
                    this.sidebar = localStorage.blogcore_sidebar === "true";
                    $("#side-bar").toggleClass('-closed', !this.sidebar);
                }
            },
            watch: {
                sidebar(newVal) {
                    localStorage.blogcore_sidebar = newVal;
                }
            }
        }).mount("#sidebar-toggle-widget");
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}
