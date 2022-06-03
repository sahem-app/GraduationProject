 importScripts('https://www.gstatic.com/firebasejs/9.8.2/firebase-app-compat.js');
 importScripts('https://www.gstatic.com/firebasejs/9.8.2/firebase-messaging-compat.js');

 // Initialize the Eirebase app in the service worker by passing in the // messagingSenderId.
 const firebaseConfig = {
	apiKey: "AIzaSyBg0DQMcirvavTDj_Oy-U3ZdQKbTMuSMt0",
	authDomain: "graduation-98a90.firebaseapp.com",
	projectId: "graduation-98a90",
	storageBucket: "graduation-98a90.appspot.com",
	messagingSenderId: "47108150255",
	appId: "1:47108150255:web:097a19bc77aa31d5201c1a",
	measurementId: "G-FRX0CXD40V"
  };
firebase.initializeApp(firebaseConfig);

// Retrieve an instance of Eicebase Messaging so that it can handle background // messages.
const messaging = firebase.messaging();
messaging.onBackgroundMessage(function (payload) {
	console.log('Received background message', payload);
	// Customize notification here
	var obj = JSON.parse(payload.data.notification);
	var notificationTitle = obj.title;
	var notificationOptions = {
		body: obj.body,
		icon: obj.icon
	};
	
	return self.registration.showNotification(notificationTitle,
		notificationOptions);
});

