const form = document.getElementById('name');
const name = document.getElementById('name');
const email = document.getElementById('email');
const phonenumber = document.getElementById('phonenumber');
const uwagi = document.getElementById('uwagi');
const data = document.getElementById('data');
const time = document.getElementById('time');


form.addEventListener('submit', e => {
		e.preventDefault();
	checkInputs();	
});


function checkInputs() {
	const nameValue = name.value.trim();
	const emailValue = email.value.trim();
	const phonenumberValue = phonenumber.value.trim();
	const uwagiValue = uwagi.value;
    const dataValue = data.value;
    const timeValue = time.value;

	if(usernameValue === '') {
		setErrorFor(name, 'To pole nie może być puste');
	} else {
		setSuccessFor(name);
	}

	if(emailValue === '') {
		setSuccessFor(email);
	} else if (!isEmail(emailValue)) {
		setErrorFor(email, 'Niepoprawny email');
	} else {
		setSuccessFor(email);
	}

	if(phonenumberValue === '') {
		setErrorFor(phonenumber, 'To pole nie może być puste');
	} else if (!isPhoneNumber(phonenumberValue)) {
		setErrorFor(phonenumber, 'Niepoprawny numer');
    } else {
		setSuccessFor(phonenumber);
	}

	setSuccessFor(uwagi);

	validDate();

	setSuccessFor(data);
	setSuccessFor(time);


}

function validDate() {
    var today = new Date().toISOString().split('T')[0];
    document.getElementsByName("date")[0].setAttribute('min', today);
}

function setErrorFor(input, message) {
	const formControl = input.parentElement;
	const small = formControl.querySelector('small');
	formControl.className = 'form-control error';
	small.innerText = message;
}

function setSuccessFor(input) {
	const formControl = input.parentElement;
	formControl.className = 'form-control success';
}

function isEmail(email) {
	return /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1, 3}\.[0-9]{1, 3}\.[0-9]{1, 3}\.[0-9]{1, 3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email);
}

function isPhoneNumber(phonenumber) {
	return /^((\(0-\d\d\) \d\d\d \d\d \d\d)|(\d\d\d \d\d\d \d\d\d))$/.test(phonenumber);
}
