

async function Register() {

    const userName = document.getElementById("userNameReg").value;
    const userPassword = document.getElementById("userPasswordReg").value;
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;

    const ans = await fetch(`https://localhost:44351/api/user`, {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "Email": userName, "Password": userPassword, "FirstName": firstName, "LastName": lastName })

    })
    DealWithRegisterAns(ans);  
}

async function LogIn() {

    const userName = document.getElementById("userNameLogIn").value;
    const userPassword = document.getElementById("userPasswordLogIn").value;
    const ans = await fetch(`https://localhost:44351/api/user/logIn`, {
        method: 'Post',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "Email": userName, "Password": userPassword })

    })
    DealWithLogInAns(ans);
}

async function Update() {

    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const firstName = document.getElementById("firstName").value;
    const lastName = document.getElementById("lastName").value;
    const id = JSON.parse(localStorage.getItem('user')).id;

    const ans = await fetch(`https://localhost:44351/api/user/${id}`, {
        method: 'Put',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ "Id": id, "Email": email, "Password": password, "FirstName": firstName, "LastName": lastName })

    })
    DealWithUpdateAns(ans);
}

async function LoadUserDetails() {

    const user = JSON.parse(localStorage.getItem('user'));
    document.getElementById("email").setAttribute('value', user.email);
    document.getElementById("password").setAttribute('value', user.password);
    document.getElementById("firstName").setAttribute('value', user.firstName);
    document.getElementById("lastName").setAttribute('value', user.lastName);
    const text = `hello ${user.firstName} ${user.lastName}`
    document.getElementById("hello").innerHTML = text; 

}


async function DealWithRegisterAns(ans) {
    const ansJson = await ans.json();

    if (ans.status == 200) {
        alert("user addded");
    }
    if (ansJson.errors) {
        if (ansJson.errors.Email)
            alert("Email not valid");
        if (ansJson.errors.Password)
            alert("password length must be between 5-20");
    }
    if (ans.status == 204)
        alert("user name already exist ");
}

async function DealWithLogInAns(ans) {
    const ansJson = await ans.json();


    if (ans.status == 200) {
        localStorage.setItem("user", JSON.stringify(ansJson));
        document.location = "https://localhost:44351/UserDetails.html";
    }
    if (ansJson.errors) {
        if (ansJson.errors.Email)
            alert("Email not valid");
        if (ansJson.errors.Password)
            alert("password length must be between 5-20");
    }
    if (ans.status == 401)
        alert("Unauthorized");
}

async function DealWithUpdateAns(ans) {
    const ansJson = await ans.json();

    if (ans.status == 200) {
        localStorage.setItem("user", JSON.stringify(ansJson));
        const text = `hello ${ansJson.firstName} ${ansJson.lastName}`
        document.getElementById("hello").innerHTML = text; 
        alert("update!!!");
    }
    if (ansJson.errors) {
        if (ansJson.errors.Email)
            alert("Email not valid");
        if (ansJson.errors.Password)
            alert("password length must be between 5-20");
    }

}