// JScript File - For Ajax based request from server
var XmlHttp;
var is_ie = (navigator.userAgent.indexOf('MSIE') >= 0) ? 1 : 0; 
var is_ie5 = (navigator.appVersion.indexOf("MSIE 5.5")!=-1) ? 1 : 0; 
var is_opera = ((navigator.userAgent.indexOf("Opera6")!=-1)||(navigator.userAgent.indexOf("Opera/6")!=-1)) ? 1 : 0; 

//netscape, safari, mozilla behave the same??? 
var is_Mozila = (navigator.userAgent.indexOf('Netscape') >= 0) ? 1 : 0;


function CreateXmlHttp(){
	//Creating object of XMLHTTP in IE
	try	{XmlHttp = new ActiveXObject("Msxml2.XMLHTTP");	}
	catch(e){try{XmlHttp = new ActiveXObject("Microsoft.XMLHTTP");} catch(oc){XmlHttp = null;}}
	//Creating object of XMLHTTP in Mozilla and Safari 
	if(!XmlHttp && typeof XMLHttpRequest != "undefined") {XmlHttp = new XMLHttpRequest();}
}

//------------------------------------------------------------------
function GetAddress_Request(nomkey, opt) {
    var Url = "../js/Ajaxfile/GetAddress_Req.ashx?nomkey=" + nomkey + "&opt=" + opt;
    if (is_ie) {
        CreateXmlHttp();
        if (XmlHttp) {
            XmlHttp.onreadystatechange = GetAddress_Repsonse;
            XmlHttp.open("POST", Url, true);
            XmlHttp.send(null);
        }
    }
    else {
        CreateXmlHttp();
        if (XmlHttp) {
            XmlHttp.onreadystatechange = GetAddress_Repsonse;
            XmlHttp.open("GET", Url, true);
            XmlHttp.send(null);
        }
    }
}





