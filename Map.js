//<![CDATA[
	var TBL; //****
	var PGS; //****
	var map = false;			
	var DETECT = navigator.userAgent.toLowerCase();
	var SPINWAIT = 0;
	var SCROLLER;
	var STATUS;
	var TMID = 0 ;
	var CURRENT = 0;
	var XML = false;
	var XMLA = false
	var DATA = false;
	var DATAA = false;
	var ICONS = false;
	var HASSTARTED = false;
	var alternator = false;
	var CURRENTPAGE = 0;
	var DATALENGTH = 0;
	var STARTDELAY = 0;
	var listeners = new Array();
	
	//NORMAL SETTING WHICH ARE OVERRIDDEN
	var WURL = '';
	var LOCALE_ADDADDRESS1	= 'The Address was located at (';
	var LOCALE_ADDADDRESS2	= '). Would you like to add the point?';
	var LOCALE_ADDRESS		= 'Address';
	var LOCALE_CITY			= 'City';	
	var LOCALE_STATE		= 'Region';
	var LOCALE_COUNTRY		= 'Country';
	var LOCALE_ZIP			= 'Postal Code';
	var LOCALE_GO			= 'Find Address';
	var LOCALE_SAVE			= 'Save';
	var LOCALE_DELETE		= 'Delete';
	var LOCALE_CANCEL		= 'Cancel';
	var LOCALE_EDIT			= 'Edit';
	var LOCALE_ICONINDEX	= 'Icon';
	var LOCALE_INDEX		= 'Index';
	var LOCALE_TIMER		= 'Sequence';
	var LOCALE_TIMERINFO	= 'Sequence Info';
	var LOCALE_LATITUDE		= 'Latitude';
	var LOCALE_LONGITUDE	= 'Longitude';
	var LOCALE_DESCRIPTION	= 'Description';
	var LOCALE_ADDADDRESS	= 'Find Address';
	var LOCALE_NEW			= 'New';
	var LOCALE_PAGEFIRST	= 'First';
	var LOCALE_PAGELAST		= 'Last';
	var LOCALE_PAGENEXT		= 'Next';
	var LOCALE_PAGEBACK		= 'Back';
	var LOCALE_STATUS1		= 'Rendering Data...';
	var LOCALE_STATUS2		= 'Fetching Data...';
	var LOCALE_STATUS3		= 'Data Failure.';
	var LOCALE_STATUS4		= 'AJAX: Asynchronous XML with Javascript is not supported by your browser.';
	var LOCALE_STATUS5		= 'There was a problem retrieving the XML data:\n';
	var LOCALE_STATUS6		= 'Updating record...';
	var LOCALE_STATUS7		= 'Finding Address...';
	var LOCALE_STATUS8		= 'Could not locate ';
	var LOCALE_STATUS9		= '. Try a different address.';
	var LOCALE_STATUS10		= 'Adding Point...';
	var LOCALE_MAPPLOT		= 'Plot';
	var LOCALE_MAPCLEAR		= 'Clear';
	var LOCALE_ZOOMHIDE		= 'Zoom (Hide)';
	var LOCALE_ZOOM			= 'Zoom (Show)';
	var LOCALE_DSEP         = '.';

	function startDataWizard()
	{
	//-----------------
	//STARTUP
	//-----------------
	if (GBrowserIsCompatible && GBrowserIsCompatible()) {
	Map_Initiate(); }
	//-----------------
	}
	function GBrowserIsCompatible()
	{
		return true;
	}
	function Map_BrowserType(string)
	{
		return DETECT.indexOf(string) + 1;
	}
	function Map_Initiate()
	{
		if (Map_BrowserType('msie') && document.readyState != 'complete')
		{ window.setTimeout('Map_Initiate();', SPINWAIT) }
		else
		{ Map_onLoad(); }
	}
	function Map_onLoad()
	{
		if (GBrowserIsCompatible && GBrowserIsCompatible()) {
			TBL		= document.getElementById("tbl"); //******
			STATUS	= document.getElementById("Status"); //*****
			PGS		= document.getElementById("pgs"); //*****
			if (window.goMap_LoadIcons) {
				goMap_LoadIcons();
			}
			//Map_mapdisplay();
			Map_Fetch();
		}
	}

	function Map_Load()
	{
		if (HASSTARTED)
		{
			if (TBL && DATA)
			{
				STATUS.innerHTML = LOCALE_STATUS1;
				HASSTARTED = false;
				Map_Render();
			}
			STATUS.innerHTML = '';
			Map_BuildPageControl();
		}
		else
		{
			HASSTARTED = true;
			window.setTimeout('Map_Load();',STARTDELAY);
		}
	}

	//-----------------
	//MANAGER FUNCTIONS
	//-----------------
	//DATA BUILDING FUNCTIONALITY
	//GET THE DATA POINTS FROM THE DOM OBJECT
	function Map_DataPoints(dom) {
		if (dom)
		{
			DATALENGTH = Map_Value(dom,"Total", 0);
			return dom.getElementsByTagName('anyType');
		}
		else
		{
			return false;
		}
	}
	function Map_Style()
	{
		strValue = '';
		strValue += '<style>\n';
		strValue += '.ColText {font-family: arial;font-size: 11px;color: black;}\n';
		strValue += '.ColHead {font-family: arial;font-size: 11px;color: white;background: #555555;font-weight: bold;}\n';
		strValue += '.ColItem {font-family: arial;font-size: 11px;color: black;background: #eeeeee;}\n';
		strValue += '.ColAltItem {font-family: arial;font-size: 11px;color: black;background: #dddddd;}\n';
		strValue += '</style>\n';
		return strValue;
	}
	//GET A VALUE OF A NODE WITHIN A DATAPOINT
	function Map_Value(obj,name,def)
	{
	   if (obj)
	   {
		nodes = obj.getElementsByTagName(name);
		if (nodes && nodes.length > 0 && nodes.item(0) && nodes.item(0).firstChild && nodes.item(0).firstChild.nodeValue)
		{
			return (nodes.item(0).firstChild.nodeValue);
		}
		else
		{
			return (def);
		}
	   }
	}
	//DATA AJAX FUNCTIONALITY
	function Map_Fetch(page)
	{
		STATUS.innerHTML = LOCALE_STATUS2;
		if (!page)
		{
			page = 0;
		}
		if (!CURRENTPAGE)
		{
			CURRENTPAGE = 0;
		}
		if (page >= 0 && ((DATALENGTH == 0 && page == 0) || ((page) <= Math.round((DATALENGTH/10) + 0.5))))
		{
				CURRENTPAGE = page;
				DATA = false;
		}
		if (!DATA)
		{
			Map_FetchStart();	
		}
		else
		{
			Map_Load();
		}
	}
	function Map_FetchStart()
	{
		try
		{
				CZOOM=-1;
				eval('MapRead();');
				eval('STATUS').innerHTML = LOCALE_STATUS2;
		}
		catch(e)
		{
				eval('STATUS').innerHTML = LOCALE_STATUS4;
		}
	}
	function Map_FetchEnd(ID)
	{
		eval('DATA = Map_DataPoints(DATA' + ID + ',' + ID + ');');
		if (DATA)
		{ 
			CURRENT = 0;
			Map_Load();
		}
	}
	function Map_DataPoints(result,ID) {
		var results;
		eval(result);
		return results;
	}

	//RENDER THE RESULTING TABLE
	function Map_Number(value,target)
	{
	    if (target!=undefined || LOCALE_DSEP != '.')
	        if (target==undefined)
	            value = (value+'').replace(/\./g,LOCALE_DSEP);
	        else
	            if (target!=LOCALE_DSEP)
	                eval('value = (value+\'\').replace(/' + LOCALE_DSEP + '/g,\'.\');');
		return value;					
	}
	function Map_Render()
	{
		var strvalue = '';
		if (TBL && DATA) {
		TBL.innerHTML = '';
			strvalue += '<table width=100% border=0 cellpadding=0 cellspacing=0 class=Normal><tr>' +
						'<td><img src="images/spacer.gif" height=1 width=16></td>' + 			
						'<td><img src="images/spacer.gif" height=1 width=16></td>' + 			
						'<td><img src="images/spacer.gif" height=1 width=16></td>' + 									
						'<td><img src="images/spacer.gif" height=1 width=50></td>' + 
						'<td><img src="images/spacer.gif" height=1 width=150></td>' +
						'<td><img src="images/spacer.gif" height=1 width=150></td>' + 
						'<td><img src="images/spacer.gif" height=1 width=50></td>' + 
						'<td><img src="images/spacer.gif" height=1 width=50></td>' + 
						'</tr><tr>' +
						'<td class=GridHeader>&nbsp;</td>' + 
						'<td class=GridHeader colspan=2>' + LOCALE_EDIT + '</td>' + 
						'<td class=GridHeader>' + LOCALE_TIMER + '</td>' + 
						'<td class=GridHeader>' + LOCALE_LATITUDE + '</td>' +
						'<td class=GridHeader>' + LOCALE_LONGITUDE + '</td>' + 
						'<td class=GridHeader>' + LOCALE_ZOOM + '</td>' + 						
						'<td class=GridHeader width=100%>' + LOCALE_ADDRESS + '</td>' +
						'</tr>';
			while (CURRENT < DATA.length)
			{
				var i = CURRENT;
				for (i = 0; CURRENT < DATA.length; i++)
				{
						if (alternator)
							cname='DataGridAlternatingItem';
						else
							cname='DataGridItem';
							alternator = !alternator;
							address = DATA[i][0];
						description = DATA[i][1];
						distance	= DATA[i][2];
						index		= DATA[i][3];
						latitude	= DATA[i][4];
						longitude	= DATA[i][5];
						iconindex	= DATA[i][6];
						timer		= DATA[i][7];
						timerinfo	= DATA[i][8];
						zoom		= DATA[i][9];
						zoomhide	= DATA[i][10];
						summarycount = DATA[i][11];
						iconvalue	= '<img src=\'' + WURL + 'spacer.gif\' width=16 height=16>';
						if (ICONS && ICONS.length > iconindex)
						{
							iconvalue	= '<img src=\'' + ICONS[iconindex].image + '\'>';
						}

						strvalue += '<tr>' + 
									'<td class=' + cname + '>' + iconvalue + '</td>' + 
									'<td class=' + cname + '>' + '<a href=\'javascript:Map_Edit(' + i + ');\'>' + '<img border=0 src=\'' + WURL + 'edit.gif' + '\'>' + '</a>' + '</td>' + 
									'<td class=' + cname + '>' + '<a href=\'javascript:Map_PointDrop(' + index + ');\'>' + '<img border=0 src=\'' + WURL + 'delete.gif' + '\'>' + '</a>' + '</td>' + 
									'<td class=' + cname + '>' + '<a href="javascript:Map_Edit(' + i + ');">' + timer + '</a></td>' + 
									'<td class=' + cname + '>' + Map_Number(latitude) + '</td>' + 
									'<td class=' + cname + '>' + Map_Number(longitude) + '</td>' + 
									'<td class=' + cname + '>' + zoom + '</td>' + 	
									'<td class=' + cname + '>' + address + '</td>' + 
									'</tr>';
						CURRENT = CURRENT + 1;
				}
			}

			CURRENT = 0;
			strvalue += '</table>';
			TBL.innerHTML = strvalue;
		}
	}
	//BUILD PAGING CONTROL FOR THE RENDERED TABLE
	function Map_BuildPageControl()
	{
		PGS.innerHTML =  '<a href="javascript:Map_Edit(-1);">' + LOCALE_NEW + '</a>&nbsp;|&nbsp;<a href="javascript:Map_Address();">' + LOCALE_ADDADDRESS + '</a>&nbsp;|&nbsp;</td>';
		PGS.innerHTML += '</td>';
		minPage = (CURRENTPAGE + 1) - 4;
		lastPage = Math.round((DATALENGTH/10) + 0.5) - 1;
		if (minPage < 1)
		{
			minPage = minPage + Math.abs(minPage)
		}
		maxPage = minPage + 6;
		if (maxPage > lastPage)
		{
			maxPage = lastPage;
		}
		if (maxPage == lastPage && maxPage - 6 > 0)
		{
			minPage = maxPage - 6;
		}
		else if (maxPage == lastPage)
		{
			maxPage = maxPage - 6 + Math.abs(maxPage - 6);
		}

		if (minPage > 0)
		{
			PGS.innerHTML += '<a href="javascript:Map_Fetch(CURRENTPAGE - 1);">' + LOCALE_PAGEBACK + '</a>&nbsp;...&nbsp;';
			PGS.innerHTML += '<a href="javascript:Map_Fetch(' + 0 + ');">' + LOCALE_PAGEFIRST + '</a>&nbsp;|&nbsp;';
		}
		else
		{
			PGS.innerHTML += '' + LOCALE_PAGEBACK + '&nbsp;...&nbsp;';
			PGS.innerHTML += '' + LOCALE_PAGEFIRST + '&nbsp;|&nbsp;';
		}
		for (x=minPage;x<=maxPage;x++)
		{	
			if (x==CURRENTPAGE)
				PGS.innerHTML +=  (x+1);
			else
				PGS.innerHTML += '<a href="javascript:Map_Fetch(' + x + ');">' + (x+1) + '</a>';
			
			PGS.innerHTML += '&nbsp;';
		}
		if (CURRENTPAGE < lastPage)
		{
			PGS.innerHTML += '|&nbsp;<a href="javascript:Map_Fetch(' + lastPage + ');">' + LOCALE_PAGELAST + '</a>&nbsp;...&nbsp;';
			PGS.innerHTML += '<a href="javascript:Map_Fetch(CURRENTPAGE + 1);">' + LOCALE_PAGENEXT + '</a>';
		}
		else
		{
			PGS.innerHTML += '| ' + LOCALE_PAGELAST + ' ... ';
			PGS.innerHTML += '' + LOCALE_PAGENEXT + '';
		}
	}
	//CLOSE THE UTILITY WINDOW (ADDRESS AND POINT)
	function Map_CloseUtility()
	{
		if (pwin)
		{ 
			try 
			{	pwin.close(); }
			catch(ex)
			{}
			pwin = false;
		}
	}
	function Map_MapPlot()
	{
		if (map && DATA)
		{
			for (mi = 0; mi < DATA.length; mi++)
			{
							address = DATA[mi][0];
						description = DATA[mi][1];
						distance	= DATA[mi][2];
						index		= DATA[mi][3];
						latitude	= DATA[mi][4];
						longitude	= DATA[mi][5];
						iconindex	= DATA[mi][6];
						timer		= DATA[mi][7];
						timerinfo	= DATA[mi][8];
						zoom		= DATA[mi][9];
						zoomhide	= DATA[mi][10];
						summarycount= DATA[mi][11];
					
					//null = ICONARRAY[iconindex]
					iconvalue = null;
					if (ICONS && ICONS.length > iconindex)
					{
						iconvalue = ICONS[iconindex];
					}
					map.addOverlay(Map_MakeMark(new GPoint(longitude, latitude),mi,description,iconvalue));
			}
		}		
	}
	function Map_MapClear()
	{
		if (map)
		{
			map.clearOverlays();
			for(i=0;i<listeners.length;i++)
			{
				try
				{
					GEvent.removeListener(listeners[i]);
				}
				catch(ex)
				{
					alert(ex);
				}
			}
			listeners = new Array();
		}
	}
	function Map_MakeMark(point, number, html, icon) {
		var marker;
		if (icon) { marker = new GMarker(point,icon); }
		else { marker = new GMarker(point); }
		// Show this marker's html in the info window when it is clicked
		listeners[listeners.length] = GEvent.addListener(marker, "click", function() {marker.openInfoWindowHtml(html);});
		return marker;
	}	
	
	//UTILITYWINDOW - EDIT A MAP POINT VALUE
	function Map_Edit(index,lat,lon,address)
	{
		Map_CloseUtility();
		
		var description;
		var distance;
		var pointid;
		var latitidue;
		var longitude;
		var iconindex;
		var timer;
		var timerinfo;
		var zoom;
		var zoomhide;

		if ((DATA && index < DATA.length) || index==-1)
		{
			if (index >= 0)
			{
							address = DATA[index][0];
						description = DATA[index][1];
						distance	= DATA[index][2];
						pointid		= DATA[index][3];
						latitude	= DATA[index][4];
						longitude	= DATA[index][5];
						iconindex	= DATA[index][6];
						timer		= DATA[index][7];
						timerinfo	= DATA[index][8];
						zoom		= DATA[index][9];
						zoomhide	= DATA[index][10];
						summarycount= DATA[index][11];

			}
			else
			{
				pointid		= -1;
				latitude	= 0.0;
				if (lat)
				{
					latitude = lat;
				}
				longitude	= 0.0;
				if (lon)
				{
					longitude = lon;
				}
				iconindex	= 0;
				timer		= 0;
				timerinfo	= '';
				description	= '';
				zoom		= 0;
				if (address == undefined)
				{
					address		= '';
				}
				zoomhide	= 17;
			}

			pwin = window.open('about:blank',null,'height=432,width=400,status=no,statusbar=0,scrollbars=0,resizable=no,toolbar=no,menubar=no,location=no');
			pwin.document.write('<html><head><title>Point Editor</title></head><body>' + Map_Style() + '<form>' + 
						'<table style="width: 100%; height:100%; border: 1px solid #cccccc;" class=ColText border=0 cellpadding=1 cellspacing=0>')

			//'<tr>' + '<td width=100px>' + LOCALE_ICONINDEX + ':</td><td width=100%><input style="width:100%;" id=txtIcon value="' + iconindex + '"></td>' + '</tr>' +
			if (ICONS && ICONS.length >	0)
			{
				pwin.document.write('<tr>' + '<td width=100px>' + LOCALE_ICONINDEX + ':</td><td width=100%>');
				for(j=0; j<ICONS.length; j++)
				{
					pwin.document.write('<input name=txtIcon type=RADIO value="' + j + '"');
					if (iconindex == j)
					{
						pwin.document.write(' checked');
					}
					pwin.document.write('/>' + '<img src=\'' + ICONS[j].image + '\'>');
				}
				pwin.document.write('</td>' + '</tr>');
			}
			else
			{
				pwin.document.write('<tr>' + '<td width=100px>' + LOCALE_ICONINDEX + ':</td><td width=100%>');
				pwin.document.write('<input style="width:300px;" id=txtIcon type=text value="' + iconindex + '">')
				pwin.document.write('</td>' + '</tr>');
			}
			pwin.document.write('<tr>' + '<td width=100px>' + LOCALE_LATITUDE + ':</td><td width=100%><input style="width:300px;" id=txtLatitude value="' + Map_Number(latitude) + '"></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_LONGITUDE + ':</td><td width=100%><input style="width:300px;" id=txtLongitude value="' + Map_Number(longitude) + '"></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_TIMER + ':</td><td width=100%><input style="width:300px;" id=txtTimer value="' + timer + '"></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_TIMERINFO + ':</td><td width=100%><input style="width:300px;" id=txtTimerInfo value="' + timerinfo + '"></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_ADDRESS + ':</td><td width=100%><textarea id=txtAddress maxlength=300 width=300px style="width:300px; height: 100px;">' + address + '</textarea></td>' + '</tr>' + 
						'<tr>' + '<td width=100px>' + LOCALE_DESCRIPTION + ':</td><td width=100%><textarea id=txtSelect maxlength=1000 width=300px style="width:300px; height: 100px;">' + description + '</textarea></td>' + '</tr>' + 
						'<tr>' + '<td width=100px>' + LOCALE_ZOOM + ':</td><td width=100%><SELECT style="width:300px;" id=txtZoom>');
						for (x=0;x<17;x++)
						{
							if (x==zoom)
								pwin.document.write('<OPTION value="' + x + '" selected=true>' + x + '</OPTION>');
							else						
								pwin.document.write('<OPTION value="' + x + '">' + x + '</OPTION>');
						}
			pwin.document.write('</SELECT></td>' + '</tr>')
			pwin.document.write(
						'<tr>' + '<td width=100px>' + LOCALE_ZOOMHIDE + ':</td><td width=100%><SELECT style="width:300px;" id=txtZoomHide>');
						for (x=0;x<17;x++)
						{
							if (x==zoomhide)
								pwin.document.write('<OPTION value="' + x + '" selected=true>' + x + '</OPTION>');
							else						
								pwin.document.write('<OPTION value="' + x + '">' + x + '</OPTION>');
						}
			pwin.document.write('</SELECT></td>' + '</tr>' +
						'<tr>' + '<td colspan=2 class=ColAltItem width=100% align=right><a href="javascript:window.opener.Map_PointUpdate(' + pointid + ',document.forms[0]);">' + LOCALE_SAVE + '</a> | ' + '<a href="javascript:window.opener.Map_PointDrop(' + pointid + ');">' + LOCALE_DELETE + '</a> | ' + '<a href="javascript:window.close();">' + LOCALE_CANCEL + '</a>' + '</td></tr>' +
						'</table>' + 
						'</form></body></html>');
		}
	}
	//HANDLE THE MAP POINT UPDATE
	function Map_PointUpdate(index,fvalue)
	{
		STATUS.innerHTML = LOCALE_STATUS6;	
		var itemvalue = 0;
		var iconcount = 0;
		radio = true;
		for (pos=0;pos<fvalue.elements.length && radio;pos++)
		{
			radio = false;
			if (fvalue.elements[pos].name == 'txtIcon')
			{
				if (fvalue.elements[pos].checked)
				{
					itemvalue = fvalue.elements[pos].value;
				}
				iconcount += 1;
				radio=true;
			}
		}
		pos-=1;
		if (iconcount == 0)
		{
			pos=1;
			itemvalue = fvalue.elements[0].value;
		}
		Map_SetPoint(index,itemvalue,fvalue.elements[pos + 2].value,fvalue.elements[pos + 3].value,fvalue.elements[pos + 4].value,fvalue.elements[pos + 5].value,fvalue.elements[pos + 1].value,fvalue.elements[pos + 0].value, fvalue.elements[pos + 6].value, fvalue.elements[pos + 7].value);
		Map_CloseUtility();
	}
	function Map_PointDrop(index)
	{
		STATUS.innerHTML = LOCALE_STATUS6;	
		Map_SetPoint(index,-9000,0,0,0,0,0,0,0,0);
		Map_CloseUtility();
	}
	//BUILD THE SET POINT URL AND HANDLE UPDATE/INSERT INTERACTION
	var SDATA;
	function Map_SetPoint(index,iconindex,sequence,sequenceinfo,address,description,longitude,latitude,zoom,zoomhide)
	{
		latitude = Map_Number(latitude,'.');
		longitude = Map_Number(longitude,'.');
		
		if (isNaN(index)) index = -1;
		if (isNaN(iconindex)) icondindex = 0;
		if (isNaN(sequence)) sequence = 0;
		if (isNaN(longitude)) longitude = 0;
		if (isNaN(latitude)) latitude = 0;
		if (isNaN(zoom)) zoom = 0;
		if (isNaN(zoomhide)) zoomhide = 0;
		var distance=0;
		SDATA = new Array();
						SDATA[0]	= address;
						SDATA[1]	= description;
						SDATA[2]	= distance;
						SDATA[3]	= index;
						SDATA[4]	= latitude;
						SDATA[5]	= longitude;
						SDATA[6]	= iconindex;
						SDATA[7]	= sequence;
						SDATA[8]	= sequenceinfo;
						SDATA[9]	= zoom;
						SDATA[10]	= zoomhide;
		eval('MapWrite();');
	}
	//UTILITYWINDOW - GET THE ADDRESS
	var pwin = false;
	var pwinurl = false;
	function Map_Address()
	{
		//OPEN A NEW WINDOW - ADD THE ADDRESS FIELDS NECESSARY
		Map_CloseUtility();
		pwin = window.open('about:blank',null,'height=172,width=400,status=no,statusbar=0,scrollbars=0,resizable=no,toolbar=no,menubar=no,location=no');
		pwin.document.write('<html><head><title>Address Locator</title></head><body>' + Map_Style() + '<form>' + 
						'<table class=ColText style="width: 100%; height:100%; border: 1px solid #cccccc;" border=0 cellpadding=0 cellspacing=0>' + 
						'<tr>' + '<td width=100px>' + LOCALE_ADDRESS + ':</td><td width=100%><input style="width:100%;" id=txtAddress1></td>' + '</tr>' +
						'<tr>' + '<td width=100px>&nbsp;</td><td width=100%><input style="width:100%;" id=txtAddress2></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_COUNTRY + ':</td><td width=100%><input style="width:100%;" id=txtCountry></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_CITY + ':</td><td width=100%><input style="width:100%;" id=txtCity></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_STATE + ':</td><td width=100%><input style="width:100%;" id=txtState></td>' + '</tr>' +
						'<tr>' + '<td width=100px>' + LOCALE_ZIP + ':</td><td width=100%><input style="width:100%;" id=txtZip></td>' + '</tr>' +
						'<tr>' + '<td colspan=2 class=ColAltItem width=100% align=right><a href="javascript:window.opener.Map_SetAddress(document.forms[0]);window.opener.setTimeout(\'Map_FetchGeo();\',0);">' + LOCALE_GO + '</a></td>' + '</tr>' +
						'</table>' + 
						'</form></body></html>');
	}
	//ADDRESS AJAX FUNCTIONALITY
	var pAddress = '';
	function Map_FetchGeoStart()
	{
		try
		{
				eval('MapGeo();');
				eval('STATUS').innerHTML = LOCALE_STATUS2;
		}
		catch(e)
		{
				alert(e.toString());
				eval('STATUS').innerHTML = LOCALE_STATUS4;
		}
	}
	function Map_FetchGeoEnd(ID)
	{
		eval('DATAA = Map_DataPoints(DATA' + ID + ',' + ID + ');');
		if (DATAA)
		{ 
			if (DATAA[0])
			{
				//SUCCESS
				Map_Edit(-1,DATAA[1], DATAA[2],DATAA[3])
			}
			else
			{
				alert('Sorry, we are unable to locate a point for that address. Please try again.');
			}
		}
	}
	//BUILD THE ADDRESS URL
	var ADDRESS;
	function Map_SetAddress(fvalues)
	{
			ADDRESS = new Array();
			ADDRESS[0] = fvalues.elements[0].value;
			ADDRESS[1] = fvalues.elements[1].value;
			ADDRESS[2] = fvalues.elements[2].value;
			ADDRESS[3] = fvalues.elements[3].value;
			ADDRESS[4] = fvalues.elements[4].value;
			ADDRESS[5] = fvalues.elements[5].value;

	}

	function Map_GeoLookup(moduleID)
	{
		var str = 'GEO:' + moduleID + '|' + pAddress;
		pAddress = '';
		return str;
	}

	//FETCH THE ADDRESS LON/LAT BASED ON URL
	function Map_FetchGeo()
	{
			if (pwin)
			{
				pwin.close();
				pwin = false;
			}
			Map_FetchGeoStart();
	}
	//THE ADDRESS HAS BEEN FOUND, PARSE THE RETURN
	function Map_AddAddress()
	{
		STATUS.innerHTML = LOCALE_STATUS7;
		if (TBL && DATAA)
		{
							address = DATAA[0][0];
						description = DATAA[0][1];
						distance	= DATAA[0][2];
						index		= DATAA[0][3];
						latitude	= DATAA[0][4];
						longitude	= DATAA[0][5];
						iconindex	= DATAA[0][6];
						timer		= DATAA[0][7];
						timerinfo	= DATAA[0][8];
						zoom		= DATAA[0][9];
						zoomhide	= DATAA[0][10];
			if (latitude == 0 && longitude == 0)
			{
				alert(LOCALE_STATUS8 + description + LOCALE_STATUS9);
			}
			else
			{
				if (DATAA)
				{
					if (window.confirm(LOCALE_ADDADDRESS1 + Map_Number(latitude) + ',' + Map_Number(longitude) + LOCALE_ADDADDRESS2))
					{
						STATUS.innerHTML = LOCALE_STATUS10;
						Map_SetPoint(-1, iconindex, timerinfo, timer, address, description, longitude, latitude, zoom, zoomhide);
					}
				}
			}
		}
		DATAA = false;
		DATA = false;
		STATUS.innerHTML = '';
	}

	function Map_clickPoint(overlay, point) {
		if (point) {
			Map_Edit(-1,point.y,point.x);		
		}
	}
//]]>