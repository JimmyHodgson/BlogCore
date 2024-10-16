﻿export const clickOutsideDirective = {
    beforeMount(el, binding) {
        el.clickOutsideEvent = function (event) {
            // Check if the click is outside the element
            if (!(el === event.target || el.contains(event.target))) {
                // Call the method provided in the binding value
                binding.value(event);
            }
        };
        document.body.addEventListener('click', el.clickOutsideEvent);
    },
    beforeUnmount(el) {
        document.body.removeEventListener('click', el.clickOutsideEvent);
    }
};

export const common = {
    delete: (url) => {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'DELETE'
            })
                .then(response => {
                    if (response.status === 200) {
                        response.json().then(data => resolve(data));
                    }
                    else {
                        response.json().then(data => reject(data));
                    }
                })
                .catch(error => reject(error));
        });
    },
    get: (url) => {
        return new Promise((resolve, reject) => {
            fetch(url)
                .then(response => response.json())
                .then(data => resolve(data))
                .catch(error => reject(error));

        });
    },
    post: (url, data) => {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => {
                    if (response.status === 200) {
                        response.json().then(data => resolve(data));
                    }
                    else {
                        response.json().then(data => reject(data));
                    }
                })
                .catch(error => reject(error));
        });
    },
    postMultiPart: (url, data) => {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'POST',
                body: data
            })
                .then(response => {
                    if (response.status === 200) {
                        response.json().then(data => resolve(data));
                    }
                    else {
                        response.json().then(data => reject(data));
                    }
                })
                .catch(error => reject(error));
        });
    },
    put: (url, data) => {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => {
                    if (response.status === 200) {
                        response.json().then(data => resolve(data));
                    }
                    else {
                        response.json().then(data => reject(data));
                    }
                })
                .catch(error => reject(error));
        });
    }
}