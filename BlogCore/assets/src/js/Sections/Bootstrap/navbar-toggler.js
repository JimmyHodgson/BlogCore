if (document.querySelector('.navbar-toggler')) {
    Promise.all([
        import('bootstrap')
    ]).then(([{ Collapse }]) => {
        // Initialize collapse if the navbar-toggler exists
        const navbarToggle = document.querySelector('.navbar-toggler');
        const navbarCollapse = new Collapse(navbarToggle);
    }).catch(error => {
        console.error("Failed to load module: ", error);
    })
}