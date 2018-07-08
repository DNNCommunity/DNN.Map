function mapReadComplete(result, ctx)
{
	alert('Read Complete' + result);
}
function Map_WriteEnd(result, ctx)
{
		Map_Fetch(CURRENTPAGE);
}
function mapReadFailure(result, ctx)
{
	alert('Read Failure!' + result);
}
function mapWriteFailure(result, ctx)
{
	alert('Write Fialure!' + result);
}
function mapServiceDataComplete(result, ctx)
{

}
function mapServiceData(moduleID)
{
	return 'GEO:' + moduleID + '|Service=1';
}
function mapReadData(moduleID)
{
	var strExtendedData = '';
	var isadmin;
	eval('isadmin=ADMINMODE' + moduleID + ';');
	if (!isadmin)
	{
		eval('strExtendedData = mapReadDataExtended' + moduleID + '();'); 
		if (strExtendedData.length > 0)
		{
			strExtendedData += '&';
		}
		eval('strExtendedData += \'latitude=\' + CLAT' + moduleID + ';');
		strExtendedData += '&';
		eval('strExtendedData += \'longitude=\' + CLON' + moduleID + ';');	
		strExtendedData += '&';
		eval('strExtendedData += \'Zoom=\' + CZOOM' + moduleID + ';');	
		strExtendedData += '&';

		eval('strExtendedData += \'distance=\' + CDIS' + moduleID + ';');	
		strExtendedData += '&';
		eval('strExtendedData += \'scale=\' + CSCALE' + moduleID + ';');		
		strExtendedData += '&';
		eval('strExtendedData += \'Page=\' + CURRENTPAGE' + moduleID + ';');		
		strExtendedData += '&';
		eval('strExtendedData += \'PerPage=\' + RPP' + moduleID + ';');	
	}
	else
	{
		strExtendedData += 'Page=' + CURRENTPAGE;
		strExtendedData += '&PerPage=' + '10';
		strExtendedData += '&Zoom=-1';	
	}
			
	return 'GET:' + moduleID + '|' + strExtendedData;
}
function mapWriteData(moduleID)
{
	var strvalue = 'SET:' + moduleID + '|' + 
						'address=' + encodeURIComponent(SDATA[0]) + '&'  +
						'description=' + encodeURIComponent(SDATA[1]) + '&'  +
						'distance=' + encodeURIComponent(SDATA[2]) + '&' +
						'index=' + encodeURIComponent(SDATA[3]) + '&' +
						'latitude=' + encodeURIComponent(SDATA[4]) + '&' +
						'longitude=' + encodeURIComponent(SDATA[5]) + '&' +
						'iconindex=' + encodeURIComponent(SDATA[6]) + '&' +
						'sequence=' + encodeURIComponent(SDATA[7]) + '&' +
						'sequenceinfo=' + encodeURIComponent(SDATA[8]) + '&' +
						'zoomshow=' + encodeURIComponent(SDATA[9]) + '&' +
						'zoomhide=' + encodeURIComponent(SDATA[10]);
	return encodeURIComponent(strvalue);
}
function mapGeoData(moduleID)
{
	var strvalue = 'GEO:' + moduleID + '|' + 
						'Address=' + encodeURIComponent(ADDRESS[0]) + '&'  +
						'Unit=' + encodeURIComponent(ADDRESS[1]) + '&'  +
						'Country=' + encodeURIComponent(ADDRESS[2]) + '&' +
						'City=' + encodeURIComponent(ADDRESS[3]) + '&' +
						'Region=' + encodeURIComponent(ADDRESS[4]) + '&' +
						'Zip=' + encodeURIComponent(ADDRESS[5]);
	return strvalue ;
}
	function Map_GetFormValue(name) 
	{ 
		   var str = ""; 
		   var obj = false;
		   obj = document.forms[0].elements[name];
		   
		   if (obj)
		   { 
			var value = "";
			   switch(obj.type) 
			   { 
					case 'text':
					case 'password':
					case 'textarea':
						value += 'F' + name + "=" + encodeURIComponent(obj.value);
						break;
					case 'select-one':
						if (obj.options.length > 0 && obj.selectedIndex >= 0) {
							value += 'F' + name + "=" + encodeURIComponent(obj.options[obj.selectedIndex].value);
						}
						break;
					case 'select-multiple':
						if (obj.length > 0) {
							var sSelValues = '';
							try
							{
								for (var iSel=0; iSel<fobj.elements[i].length; iSel++ )
								{
									if (obj.options[iSel].selected == true)
									{
										if (sSelValues != '')
											   sSelValues += "&F" + name + '=';
										sSelValues += encodeURIComponent(obj.options[iSel].value);
									}
								}
							}
							catch (err)
							{
							}
							value += 'F' + name + '=' + sSelValues;
						}
						break;
					case 'hidden':
						value += 'F' + name + '=' + encodeURIComponent(obj.value); 
						break;
					case 'radio':
						if (obj.checked)
						{
							value += 'F' + name + "=" + encodeURIComponent(obj.value); 
						}
						break;
					case 'checkbox':
						if (fobj.elements[i].checked)
						{
							value += 'F' + name + "=" + encodeURIComponent(obj.value); 
						}
						break;
					default:
						//alert(fobj.elements[i].type);
			   } 
				if (value != "")  
				{ if (str!="")
					  str += "&";
				 str += value;
				}
		   } 
		   return str; 
	}
	function Map_GetQueryStringValue(name)
	{
		var QRY='';  var cleanURL; var urlParts; var recordElement = false; var isValue = false; var isKey = true; var pathQuery = "";var isSkipped=false;
		var str = '';
		if (document.location.search.length > 0)
		{
			  //trim the question mark
			  QRY = document.location.search.substr(1);
		}
		
		var QRYpairs = QRY.split('&');
		QRY = new Array();
		for (i=0;i<QRYpairs.length;i++)
		{
			var QRYKey = QRYpairs[i].split('=');
			if (QRYKey[0].toLowerCase()==name.toLowerCase())
			{
				i = QRYpairs.length+1;
				str = 'Q' + name + '=' + QRYKey[1];
			}
		}
		
		cleanURL = document.location.pathname;
		urlParts = cleanURL.split("/");
		//looping to leave last item which is page name (<.length-1)
		for (var i=0;i<urlParts.length-1;i++) 
		{
		   if (!recordElement && urlParts[i].toLowerCase()=="tabid") 
		   { recordElement =true; }
		   if (recordElement)
		   {
				if (isKey) 
				{ 
					if (urlParts[i].toLowerCase()==name.toLowerCase())
					{
						isValue = true;
					}
				}
				else
				{	if (isValue)
					{
						str = 'Q' + name + '=' + urlParts[i];
						i = urlParts.length + 1;
					}
				}
				isKey = !isKey;
		   }
		}
		return str;
	}
