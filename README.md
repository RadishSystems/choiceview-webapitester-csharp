Overview
--------
ChoiceView is a Communications-as-a-Service (CAAS) platform that allows visual information to be sent from a contact center agent or IVR to mobile users equipped with the ChoiceView app.

Description
-----------
The [ChoiceView IVR API](http://www.radishsystems.com/for-developers/for-ivr-developers/) is a REST-based service that provides IVR systems and telephony services access to ChoiceView features. 

This repository contains source code for a simple .Net Windows Form application that uses the Microsoft HTTP Client libraries in .Net 4 to make IVR API requests. The application is written in C#, and can be compiled using Visual Studio 2010 or later.

Dependencies
------------
All of the packages needed to make IVR API calls can be found in the [NuGet Gallery](http://www.nuget.org/). The project is configured to automatically download the dependencies from the NuGet Gallery when the project is built. The list of NuGet packages is in the [packages.config](https://github.com/radishsystems/choiceview-webapitester-csharp/blob/master/ApiTester/packages.config) file.

LICENSE
-------
[MIT License](https://github.com/radishsystems/choiceview-webapitester-csharp/blob/master/LICENSE)

How to Build
------------
The _choiceview-webapitester-csharp_ project is built and maintained using Visual Studio. All of the build dependencies are in the public NuGet Gallery.  You'll need to [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget) in order to get the dependencies. 

The _main_ branch is built with the latest version of Visual Studio, currently Visual Studio 2013. The minimum version of .Net supported by the _main_ branch is .Net 4.5. The _VS2010_ branch is built with Visual Studio 2010 and supports .Net 4.0. 

Usage
-----
To use the ApiTester program, you must have a mobile device with the latest ChoiceView Client application installed.  You should know the phone number of the mobile device, or the phone number that the ChoiceView client is configured to use.  

Usually, you will use the ApiTester program with the development ChoiceView server for evaluations and testing.  The server address is _cvnet2.radishsystems.com_. Login credentials for the development server can be requested from our support team. The server address and the login credentials must be manually entered into the ApiTester configuration file. This can be done from within Visual Studio by going to the ApiTester project properties page, selecting the settings tab, and entering the values for the server address and credentials.  The client must be configured to connect to the development ChoiceView server.  On iOS devices, press Settings, then Advanced, then change the server field to _cvnet2.radishsystems.com_. On Android devices, press the menu button, then Settings, then scroll down to the server field and change the value to _cvnet2.radishsystems.com_.

To start the ApiTester program, you can either start it from within Visual Studio, or from an Windows Explorer window.  After the application starts, enter the mobile phone number. The ChoiceView server uses this value to find the mobile client session. You can also enter a separate unique call id that will be used by the server to track the ChoiceView session. This value is optional. The call id is useful for identifying a specific ChoiceView session out of multiple sessions with the same Caller ID. After entering the mobile phone number and optional call id, press the Start Session button.  You must also open the ChoiceView application on your mobile device and press the Start button for the session to be established.

When the ChoiceView session is active, ApiTester will display the current status of the ChoiceView session.

Radish is providing this application and the source code to show IVR developers how to call the IVR API with one of the readily available .Net HTTP client libraries. It is not intended to for production use, and there is no express or implied warantee. Contact us if you have problems or need help in using or modifying this application.

Contact Information
-------------------
If you want more information on ChoiceView, or want access to the ChoiceView REST API, contact us.

[Radish Systems, LLC](http://www.radishsystems.com/support/contact-radish-customer-support/)

-	support@radishsystems.com
-	darryl@radishsystems.com
-	+1.720.440.7560
