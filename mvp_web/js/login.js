import constants from "./constants.js";

loadData();

function loadData() {
    let loginCode = getLoginId();

    fetch(new URL(`/Login/${loginCode}`, constants.baseUrl))
        .then((response) =>
            response.json().then(setContent))
        .catch((response) => displayRequestFailed(response));
}

function getLoginId() {
    let queryString = window.location.search;
    let urlParams = new URLSearchParams(queryString);

    return urlParams.get("loginKey");
}

function setContent(authenticationResponse) {
    let titleElement = document.createElement("h1");
    titleElement.innerText = `Welcome to the site of ${authenticationResponse.userId}!`;

    let textElement = document.createElement("p");
    textElement.innerText = `${authenticationResponse.key}, ${authenticationResponse.token}, ${authenticationResponse.userId}`;

    let contentElement = document.getElementById("content");
    contentElement.innerHTML = "";
    contentElement.appendChild(titleElement);
    contentElement.appendChild(textElement);
}

function displayRequestFailed(reason) {
    console.log(reason);
}