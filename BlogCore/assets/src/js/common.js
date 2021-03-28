const common = {
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
                body: data
            })
                .then(response => resolve(response))
                .catch(error => reject(error));
        });
    },
    postMultiPart: (url, data) => {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'POST',
                
                body: data
            })
                .then(response => resolve(response))
                .catch(error => reject(error));
        });
    }
}