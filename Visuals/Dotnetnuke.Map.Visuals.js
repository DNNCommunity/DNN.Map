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
	var LOCALE_STATE		= 'State';
	var LOCALE_ZIP			= 'Zip';
	var LOCALE_GO			= 'Find Address';
	var LOCALE_SAVE			= 'Save';
	var LOCALE_DELETE		= 'Delete';
	var LOCALE_CANCEL		= 'Cancel';
	var LOCALE_EDIT			= 'Edit';
	var LOCALE_ICONINDEX	= 'Icon';
	var LOCALE_INDEX		= 'Index';
	var LOCALE_TIMER		= 'Timer';
	var LOCALE_TIMERINFO	= 'Timer Info';
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
	var LOCALE_ZOOM			= 'Zoom';

	function startDataWizard()
	{
	//-----------------
	//STARTUP
	//-----------------
	if (GBrowserIsCompatible && GBrowserIsCompatible()) {
	gWiz_Initiate(); }
	//-----------------
	}
	function GBrowserIsCompatible()
	{
		return true;
	}
	function Default(name,id)
	{
		switch(name)
		{
			case 'SEARCH': document.getElementById(id).value=DEFAULT_SEARCHTEMPLATE; break;
			case 'EMPTY':  document.getElementById(id).value=DEFAULT_SEARCHNORESULTS; break;
			case 'ITEM':  document.getElementById(id).value=DEFAULT_DIRECTORYITEMTEMPLATE; break;
		}
	}
	function gWiz_BrowserType(string)
	{
		return navigator.userAgent.toLowerCase().indexOf(string) + 1;
	}
	function gWiz_Initiate()
	{
		if (gWiz_BrowserType('msie') && document.readyState != 'complete')
		{ window.setTimeout('gWiz_Initiate();', SPINWAIT) }
		else
		{ gWiz_onLoad(); }
	}
	function gWiz_onLoad()
	{
		if (GBrowserIsCompatible && GBrowserIsCompatible()) {
			TBL		= document.getElementById("tbl"); //******
			STATUS	= document.getElementById("Status"); //*****
			PGS		= document.getElementById("pgs"); //*****
			if (window.Map_LoadIcons) {
				Map_LoadIcons();
			}
			gWiz_mapdisplay();
		}
	}
	function gWiz_MakeMark(point, number, html, icon) {
		var marker;
		if (icon) { marker = new GMarker(point,icon); }
		else { marker = new GMarker(point); }
		// Show this marker's html in the info window when it is clicked
		listeners[listeners.length] = GEvent.addListener(marker, "click", function() {marker.openInfoWindowHtml(html);});
		return marker;
	}	
	function gWiz_mapdisplay() {
	// Click Handling
	//
	// When you click the map, we create a new marker at that point. When you
	// click a marker, we remove it from the map.
	var objMap 
	objMap = document.getElementById("map");
	try
		{
	map = new GMap(objMap);
		}
	catch (ex)
		{
			map==false;
			if (objMap)
			{	objMap.style.display = 'none';
				objMap.style.visibility = 'hidden';
			}
				
		}
	if (map)
	{
		map.addControl(new GSmallMapControl());
		map.addControl(new GMapTypeControl());
			
		// Add Starting Point
		var startP = new GPoint(LOCALE_SLON,LOCALE_SLAT);
		var startPDesc = LOCALE_SDESCRIPTION;
		var marker;
		var iconvalue;

		map.centerAndZoom(startP, parseInt(LOCALE_SZOOM));

		iconvalue = null;
		if (ICONS && ICONS.length > 0)
		{
			iconvalue = ICONS[0];
		}

		if (iconvalue) { marker = new GMarker(startP,iconvalue); }
		else { marker = new GMarker(startP); }

		map.addOverlay(marker);
		GEvent.addListener(map, 'click', gWiz_clickPoint);
			
		//Show Starting Point Info
		marker.openInfoWindowHtml(startPDesc);
		}
	}
	function gWiz_clickPoint(overlay, point) {
		if (point) {
			iconvalue = null;
			if (ICONS && ICONS.length > 0)
			{
				iconvalue = ICONS[0];
			}

			if (iconvalue) { marker = new GMarker(point,iconvalue); }
			else { marker = new GMarker(point); }
			map.addOverlay(marker);

			Map_Edit(-1,point.y,point.x);		
		}
	}


//]]>