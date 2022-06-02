async function Get(url) {
    try {
        let resp = await fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json"
            }
        });

        return await resp.json();
    }
    catch (e) {
        console.log(e);
        return false;
    }
}

async function Post(url, obj) {
    try {
        let resp = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(obj),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        return await resp.json();
    }
    catch (e) {
        console.log(e);
        return false;
    }
}

async function Put(url, obj) {
    try {
        let resp = await fetch(url, {
            method: 'PUT',
            body: JSON.stringify(obj),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        return true;
    }
    catch (e) {
        console.log(e);
        return false;
    }
}

async function Delete(url) {
    try {
        let resp = await fetch(url, {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer " + access_token
            }
        });

        return true;
    }
    catch (e) {
        console.log(e);
        return false;
    }
}
