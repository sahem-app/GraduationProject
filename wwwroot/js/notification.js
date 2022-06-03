import { initializeApp } from "https://www.gstatic.com/firebasejs/9.8.2/firebase-app.js";
import { getMessaging, onMessage, getToken } from "https://www.gstatic.com/firebasejs/9.8.2/firebase-messaging.js";
const firebaseConfig = {
	apiKey: "AIzaSyBg0DQMcirvavTDj_Oy-U3ZdQKbTMuSMt0",
	authDomain: "graduation-98a90.firebaseapp.com",
	projectId: "graduation-98a90",
	storageBucket: "graduation-98a90.appspot.com",
	messagingSenderId: "47108150255",
	appId: "1:47108150255:web:097a19bc77aa31d5201c1a",
	measurementId: "G-FRX0CXD40V"
};

const app = initializeApp(firebaseConfig);
const messaging = getMessaging();

getToken(messaging, { vapidKey: 'BH550AAYrznO5nPrUM-T06SOEQZpgvDgQ0-jzoVlgm1uHXWklfAb6sm8YmJ9jJkZgpG3oldENsT_kEn4hGU-D5w' }).then((currentToken) => {
	if (currentToken) {
		var inputTag = document.getElementById('fcmToken')
		if (inputTag != null)
			inputTag.setAttribute('value', currentToken);
		console.log(currentToken);
		// Send the token to your server and update the UI if necessary
	} else {
		// Show permission request UI
		console.log('No registration token available. Request permission to generate one.');
		// ...
	}
}).catch((err) => {
	console.log('An error occurred while retrieving token. ', err);
});

onMessage(function (payload) {
	var obj = JSON.parse(payload.data.notification);
	var notification = new Notification(obj.title, {
		icon: obj.icon,
		body: obj.body
	})
});

