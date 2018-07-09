//<![CDATA[
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
	function Map_BrowserType(ID,string)
	{
		return eval('DETECT' + ID).indexOf(string) + 1;
	}

	function Map_GetPoint(latitude,longitude)
	{
		if (isFinite(latitude) && isFinite(longitude))
		{
			return new GLatLng(parseFloat(latitude),parseFloat(longitude));
		}
		else
		{
			return null;
		}
	}
	function gWiz_MapClear(ID)
	{
		var MAP = eval('MAP' + ID);
	    if (MAP) 
		{
			MAP.clearOverlays();
			for(i=0;i<eval('LISTENERS' + ID).length;i++)
			{
	        try
				{
					GEvent.removeListener(eval('LISTENERS' + ID + '[i]'));
				}
				catch(ex)
				{
					alert(ex);
				}
			}
			eval('LISTENERS' + ID + ' = new Array();');
		}
	}
	function Map_PlotBasePoint(ID)
	{
		if (eval('DDESC' + ID) && eval('DDESC' + ID).length > 0)
		{
			var currentMarker;
			currentMarker = Map_CreateMarker(ID,Map_GetPoint(eval('DLAT' + ID),eval('DLON' + ID)),0,eval('DDESC' + ID),eval('ICONS' + ID)[eval('DICON' + ID)]);
			if (currentMarker!=null)
			{
				eval('MAP' + ID).addOverlay(currentMarker);
			}
		}
	}
	var pointCount = 0;
	function Map_CreateMarker(ID,point, number, html, icon) {
		if (point != null)
		{
		pointCount = pointCount + 1;
		var marker;
		if (icon) { 
				marker = new GMarker(point,icon); 
			}
		else { 
				marker = new GMarker(point); 
			}
		// Show this marker's html in the info window when it is clicked
		eval('LISTENERS' + ID)[eval('LISTENERS' + ID).length] = GEvent.addListener(marker, "click", function() {marker.openInfoWindowHtml(html);});
		return marker;
		}
		else
		{
			return null;
		}
	}
	function Map_AddPoint(obj,ID,latitude,longitude,iconindex)
	{
		eval('CURRENT' + ID + '= CURRENT' + ID + ' + 1;');
		if (obj)
		{
			var description = obj.innerHTML;
			var point = Map_GetPoint(latitude,longitude);
			var changeview = false;
			if (eval('DCCLEAR' + ID))
			{
				gWiz_MapClear(ID);
			}

			var cid = Map_Current(ID);
			if (eval('window.DCPLOT' + ID))
			{
				var currentMarker = Map_CreateMarker(ID,point,cid,description,eval('ICONS' + ID)[iconindex]);
				if (currentMarker!=null)
				{
					eval('MAP' + ID).addOverlay(currentMarker);
				}
			}

			if (eval('window.DCZOOM' + ID) || eval('DCZOOM' + ID) == 0)
			{
				eval('CZOOM' + ID + ' = ' + eval('DCZOOM' + ID) + ';');
				changeview = true;
			}
			if (eval('DCPAN' + ID))
			{
				eval('CLAT' + ID + ' = ' + latitude + ';');
				eval('CLON' + ID + ' = ' + longitude + ';');
				changeview = true;
			}

			if (changeview)
			{
				Map_SetFocus(ID);
			}
		}
	}
	function Map_DisplayPoint(ID,latitude,longitude,iconindex,timer,timerinfo,description,distance,summarycount)
	{
		if (eval('window.DIRECTORY' + ID))
		{
			var DIRECTORY = eval('DIRECTORY' + ID);
			var DFORMAT = eval('DFORMAT' + ID);
			DFORMAT = DFORMAT.replace('[LATITUDE]',latitude);
			DFORMAT = DFORMAT.replace('[LONGITUDE]',longitude);
			DFORMAT = DFORMAT.replace('[DESCRIPTION]',description);
			DFORMAT = DFORMAT.replace('[SUMMARYCOUNT]',summarycount);
			DFORMAT = DFORMAT.replace('[ICON]',eval('ICONS' + ID)[iconindex].image);
			DFORMAT = DFORMAT.replace('[DISTANCE]',(distance * 1).toFixed(1) + 'mi');
			var i = Map_Current(ID);
			if (i % 2 == 0)
			{
				DFORMAT = DFORMAT.replace('[CLASS]',eval('DICLASS' + ID));
				DFORMAT = DFORMAT.replace('[ONMOUSEOUT]','onmouseout="this.className=\'' + eval('DICLASS' + ID) + '\'"');
			}
			else
			{
				DFORMAT = DFORMAT.replace('[CLASS]',eval('DIACLASS' + ID));
				DFORMAT = DFORMAT.replace('[ONMOUSEOUT]','onmouseout="this.className=\'' + eval('DIACLASS' + ID) + '\'"');
			}
			DFORMAT = DFORMAT.replace('[ONMOUSEOVER]','onmouseover="this.className=\'' + eval('DIHLASS' + ID) + '\'"');
			DFORMAT = DFORMAT.replace('[ONCLICK]','onclick="Map_AddPoint(this,' + ID + ',' + latitude + ',' + longitude + ',' + iconindex + ')"');
			DIRECTORY.innerHTML += DFORMAT;
		}	
	}

	function Map_PlotPoint(ID, value,clear,all,showstatusinfo)
	{
		if (eval('MAP' + ID) && eval('DATA' + ID))
		{
			if (clear==1) { 
				// Clear The Points
				gWiz_MapClear(ID);
				// Add the Center Point
				Map_PlotBasePoint(ID);
				if (eval('SCROLLER' + ID))
					eval('SCROLLER' + ID).style.left=0;
			}
			if (Map_Current(ID) < eval('DATA' + ID).length)
			{
				var i = Map_Current(ID);
				var mData = Map_Data(ID);
				var i = eval('CURRENT' + ID);

				for (i = 0; eval('CURRENT' + ID) < eval('DATA' + ID).length && i < eval('ANIMATEGROUP' + ID); i++)
				{
						var j = Map_Current(ID);
						var mDataJ = mData[j];
						address = mDataJ[0];
						description = mDataJ[1];
						distance	= mDataJ[2];
						index		= mDataJ[3];
						latitude	= mDataJ[4];
						longitude	= mDataJ[5];
						iconindex	= mDataJ[6];
						timer		= mDataJ[7];
						timerinfo	= mDataJ[8];
						zoom		= mDataJ[9];
						zoomhide	= mDataJ[10];
						summarycount = mDataJ[11];

						if (eval('STATUS' + ID) && showstatusinfo) {
							eval('STATUS' + ID).innerHTML = timerinfo;
						}

						if (!eval('window.DCPLOT' + ID))
						{
							description = Map_FormatDescription(ID,address,description,distance,index,latitude,longitude,iconindex,timer,timerinfo,zoom,zoomhide,summarycount);

							var currentMarker = Map_CreateMarker(ID,Map_GetPoint(latitude,longitude),Map_Current(ID),description,eval('ICONS' + ID)[iconindex]);

							if (currentMarker!=null)
							{

								eval('MAP' + ID).addOverlay(currentMarker);
							}
						}	
						if (isFinite(latitude) && isFinite(longitude))
						{
							Map_DisplayPoint(ID,latitude,longitude,iconindex,timer,timerinfo,description,distance);
						}
					//eval('CURRENT' + ID + '= CURRENT' + ID + ' + 1;');
						eval('CURRENT' + ID + '= CURRENT' + ID + ' + 1;');					
				}
			}
		}
	}
	function Map_FormatDescription(ID,address,description,distance,index,latitude,longitude,iconindex,timer,timerinfo,zoom,zoomhide,summarycount)
	{
			var DFORMAT = eval('MapPointFormat' + ID);
			DFORMAT = DFORMAT.replace('[LATITUDE]',latitude);
			DFORMAT = DFORMAT.replace('[LONGITUDE]',longitude);
			DFORMAT = DFORMAT.replace('[DESCRIPTION]',description);
			DFORMAT = DFORMAT.replace('[ADDRESS]',address);
			DFORMAT = DFORMAT.replace('[TIMER]',timer);
			DFORMAT = DFORMAT.replace('[TIMERINFO]',timerinfo);
			DFORMAT = DFORMAT.replace('[SUMMARYCOUNT]',summarycount);
			DFORMAT = DFORMAT.replace('[ZOOMSHOW]',zoom);
			DFORMAT = DFORMAT.replace('[ZOOMHIDE]',zoomhide);
			DFORMAT = DFORMAT.replace('[ICON]',eval('ICONS' + ID)[iconindex].image);
			DFORMAT = DFORMAT.replace('[DISTANCE]',(distance * 1).toFixed(1) + 'mi');
			return DFORMAT;
	}
	function Map_Reset(ID)
	{
		Map_PlotPoint(ID,0,1,0,eval('SHOWTIMER' + ID));
	}
	function Map_SetFocus(ID)
	{
		eval('MAP' + ID).centerAndZoom(new GPoint(eval('CLON' + ID), eval('CLAT' + ID)), eval('CZOOM' + ID));	
	}
	function Map_Click(ID,overlay,point)
	{
		if (!overlay)
		{
			eval('CURRENT' + ID + ' = 0;');
			eval('CLON' + ID + ' = point.x;');
			eval('CLAT' + ID + ' = point.y;');
			eval('CZOOM' + ID + ' = MAP' + ID + '.getZoomLevel();');
			var xmap;
			eval('xmap = MAP' + ID + ';');

			Map_SetFocus(ID);
			eval('DATA' + ID + ' = false;');
			if (eval('DATALENGTH' + ID)  >= 0)
			{
				eval('CURRENTPAGE' + ID + ' = 0;');
				Map_Fetch(ID,eval('CURRENTPAGE' + ID));
			}
			else
			{
				Map_Fetch(ID);
			}
		}
	}
	function Map_Go(ID,a,o,z)
	{
			eval('CURRENT' + ID + ' = 0;');
			eval('CLON' + ID + ' = o;');
			eval('CLAT' + ID + ' = a;');
			eval('CZOOM' + ID + ' = z;');
         eval('MAP' + ID).centerAndZoom(new GPoint(o,a), z);
			Map_SetFocus(ID);
			eval('DATA' + ID + ' = false;');
			Map_Fetch(ID);
	}
	function Map_Zoom(ID,oz,z)
     {   
             var notfound = true;
             var NZOOM = eval('XZOOM' + ID);
			 var ZOOMS = eval('ZOOMS' + ID);
             for(i=ZOOMS.length;i>0 && notfound;i--)
             {
                 if (z >= ZOOMS[i-1])
                 {
                     NZOOM = ZOOMS[i-1];
                     i = 0;
                     notfound = false;
                 }
             }
             if (notfound)
             {
                 NZOOM=0;
             }
             if (eval('XZOOM' + ID) != NZOOM) { 
                 gpnt = eval('MAP' + ID).getCenterLatLng();
                 eval('CLAT' + ID + ' = gpnt.y;');
                 eval('CLON' + ID + ' = gpnt.x;');
        		 eval('CZOOM' + ID + ' = MAP' + ID + '.getZoomLevel();');
        		 eval('XZOOM' + ID + ' = NZOOM;');
        		 eval('DATA' + ID + ' = false;');
                 gWiz_MapClear(ID);
        			Map_Fetch(ID);
             }
        }
 function Map_getDelay(ID)
	{
		var rValue = 1;
	    if (eval('USETIMER' + ID) && eval('DATA' + ID) && Map_Current(ID) < eval('DATA' + ID).length && Map_Current(ID)>0)
	    {
						var aNode0 = eval('DATA' + ID)[Map_Current(ID) - 1];
						var aNode1 = eval('DATA' + ID)[Map_Current(ID)];
						var timer0		= aNode0[6]; //Map_Value(aNode0,"Timer",0);
						var timer1		= aNode1[6]; //Map_Value(aNode1,"Timer",0);
	    if (!isNaN(timer0) && !isNaN(timer1) && timer1 > timer0) { rValue = timer1 - timer0; }
	    return rValue; }
	}
	function Map_AnimateDelay(ID)
	{
		try
		{
			return eval('ANIMATEDELAY' + ID);			
		}
		catch (ex)
		{
			return 0;
		}
	}
	function Map_Current(ID)
	{
		return eval('CURRENT' + ID);
	}
	function Map_AnimateGroup(ID)
	{
		return eval('CURRENT' + ID);
	}	
	function Map_Data(ID)
	{
		return eval('DATA' + ID);
	}
	
	function Map_Play(ID)
	{
		if (eval('AUTOSTART' + ID))
		{
		if (eval('MAP' + ID) && eval('DATA' + ID) && eval('PLAY' + ID)==1) {
			if (Map_Current(ID) == 0) 
				{ Map_PlotPoint(ID,Map_Current(ID),1,0,eval('SHOWTIMER' + ID)) } 
			else 
				{ Map_PlotPoint(ID,Map_Current(ID),0,0,eval('SHOWTIMER' + ID)) };
			
			leftvalue = 0;
			if (Map_Current(ID) > 0)
			{
				leftvalue = (100 / eval('DATA' + ID).length) * Map_Current(ID);
			}
			
			if (leftvalue > 100) leftvalue=100;
			
			if (eval('SCROLLER' + ID)) eval('SCROLLER' + ID).style.left= leftvalue + '%';
			
			if (Map_Current(ID) < eval('DATA' + ID).length) {
				eval('STATUS' + ID).style.display='block';
				window.setTimeout('Map_Play(' + ID + ');',Map_AnimateDelay(ID) * Map_getDelay(ID)); 
			}
			else
			{
				eval('STATUS' + ID).style.display='none';
			}
		}
		if (Map_Current(ID) > 0)
		{
         if (!eval('SHOWTIMER' + ID)) {
		    	eval('STATUS' + ID).innerHTML = 'Point ' + Map_Current(ID) + '/' + eval('DATA' + ID).length;
         }
		}
	  }
	  	else
		{
			eval('AUTOSTART' + ID + ' = true;');
		}
	}
	var pcount = 0
	function Map_Plot(ID)
	{
		eval('Map_SetDirectory' + ID + '();');
		
		eval('STATUS' + ID).innerHTML = 'Plotting Points...';
		eval('HASSTARTED' + ID + ' = true;');
		if (eval('HASSTARTED' + ID))
		{
			if (eval('MAP' + ID) && eval('DATA' + ID))
			{
				eval('HASSTARTED' + ID + ' = false;');
				eval('PLAY' + ID + ' = 1;'); 
				pcount += 1;
				if (eval('DATALENGTH' + ID) >= 0)
				{
					Map_Page(ID);
				}
				
				//CLEAR THE CURRENT PAGE INFO
				if (eval('window.DIRECTORY' + ID))
				{
					eval('DIRECTORY' + ID + '.innerHTML = \'\';');
					if (eval('DATALENGTH' + ID) == 0)
					{
						var DIR = false;
						eval('DIR = DIRECTORY' + ID + ';');
						DIR.innerHTML = eval('DNRT' + ID);
					}
				}
				Map_Play(ID);
			}
			eval('STATUS' + ID).style.display = 'none';
		}
		else
		{
			eval('HASSTARTED' + ID + ' = true;');
			window.setTimeout('Map_Plot(' + ID + ');',eval('STARTDELAY' + ID));
		}
	}
	function Map_Fetch(ID,page)
	{
		var CURRENTPAGE;
		var DATALENGTH;
		var RPP;

		if (page || page == 0) {
			eval('CURRENTPAGE = CURRENTPAGE' + ID + ';');
			eval('DATALENGTH = DATALENGTH' + ID + ';');
			eval('RPP = RPP' + ID + ';');
			
			if (!CURRENTPAGE)
			{
				eval('CURRENTPAGE' + ID + '=0;');
			}
			if (page >= 0 && ((DATALENGTH == 0 && page == 0) || ((page) <= Math.round((DATALENGTH/RPP) + 0.5))))
			{
					eval('CURRENTPAGE' + ID + '=' + page + ';');		
					eval('DATA' + ID + '=false;');
			}
		}
	
		
				
		var DATA;
		eval('DATA = DATA' + ID + ';');
	
		eval('STATUS' + ID).style.display = 'block';
		eval('STATUS' + ID).innerHTML = 'Initiating Request...';
		eval('HASSTARTED' + ID + ' = false;');
		if (!eval('DATA' + ID))
		{
			if (page || page == 0)
			{
				Map_FetchStart(ID,page);	
			}
			else
			{
				Map_FetchStart(ID);		
			}
		}
		else
		{
			Map_Plot(ID);
		}
	}
	function Map_FetchStart(ID,page)
	{
		try
		{
				eval('MapRead' + ID + '();');
				eval('STATUS' + ID).innerHTML = 'Fetching...';
		}
		catch(e)
		{
			alert(e.message);
				eval('STATUS' + ID).innerHTML = 'AJAX: Asynchronous XML with Javascript is not supported by your browser.';
		}	
	}
	function Map_FetchEnd(ID)
	{
		eval('DATA' + ID + ' = Map_DataPoints(DATA' + ID + ',' + ID + ');');
		if (eval('DATA' + ID))
		{ 
			eval('CURRENT' + ID + '=0;');
			Map_Plot(ID);
		}
	}
	function Map_DataPoints(result,ID) {
		var results;
		eval(result);
		return results;
	}
	
	function Map_Initiate(ID)
	{
		if (Map_BrowserType(ID,'msie') && document.readyState != 'complete')
		{ window.setTimeout('Map_Initiate(' + ID + ');', eval('STARTDELAY' + ID)) }
		else
		{ Map_Load(ID); }
	}
    function Map_Load(ID)
    {
        if (window.GBrowserIsCompatible && GBrowserIsCompatible()) {
        eval('MAP' + ID + ' = new GMap(document.getElementById("map' + ID + '"));');
        eval('Map_LoadIcons' + ID + '();');
        eval('Map_SetControls' + ID + '();');

        if (eval('window.SEARCHBTN' + ID))
        {
            eval('SEARCHBTN' + ID + '.onclick = function () {Map_Search(' + ID + ')};')
            var ln = 0;
            eval('ln = SEARCHTXT' + ID + '.value.length;')
            if (ln > 0)
            {
            Map_SetFocus(ID);         
            Map_PlotBasePoint(ID);

            Map_Search(ID);
            }
            else
            {
            Map_SetFocus(ID);         
            Map_PlotBasePoint(ID);
            if (eval('DATALENGTH' + ID)  >= 0)
            {
                            Map_Fetch(ID,eval('CURRENTPAGE' + ID));
            }
            else
            {
                            Map_Fetch(ID);
            }
            }

        }
        else
        {
            Map_SetFocus(ID);         
            Map_PlotBasePoint(ID);
            if (eval('DATALENGTH' + ID)  >= 0)
            {
                            Map_Fetch(ID,eval('CURRENTPAGE' + ID));
            }
            else
            {
                            Map_Fetch(ID);
            }

        }
        }
    }

	function Map_Startup(ID)
	{
		if (window.GBrowserIsCompatible && window.GBrowserIsCompatible()) {
		Map_Initiate(ID); }
	}

//ADDRESS SELECTION FUNCTIONALITY
	//UTILITYWINDOW - GET THE ADDRESS
	var geocoder = false;
	function gWiz_FetchAddressStart(ID, Address)
	{
		if(!geocoder)
		{
			geocoder = new GClientGeocoder;
		}
		eval('geocoder.getLatLng(Address,function(point) { gWiz_FetchAddressEnd(' + ID + ',point); });');
	}
	function gWiz_FetchAddressEnd(ID,point)
	{
		if (!point)
		{
			alert('Sorry, we are unable to locate a point for that address. Please try again.');
		}
		else
		{
			Map_UseAddress(ID,point.lat(),point.lng());
		}
	}

	//BUILD PAGING CONTROL FOR THE RENDERED TABLE
	function Map_Page(TM)//
	{
			var CURRENTPAGE = 0;
			var DATALENGTH = 0;
			var RPP = 0;
			var PGS = false;
			
			var PAGEITEMCLASS = '';
			var PAGEITEMLINKCLASS = '';
			var PAGEITEMHOVERCLASS = '';
			
			eval('CURRENTPAGE = CURRENTPAGE' + TM + ';');
			eval('DATALENGTH = DATALENGTH' + TM + ';');
			eval('RPP = RPP' + TM + ';');
			eval('PGS = PAGER' + TM + ';');
			eval('PAGEITEMCLASS = DPCLASS' + TM + ';');
			eval('PAGEITEMLINKCLASS = DPLCLASS' + TM + ';');
			eval('PAGEITEMHOVERCLASS = DPHCLASS' + TM + ';');
			if (PGS)
			{
				PGS.innerHTML = '';
				if (DATALENGTH > 2)
				{
					PGS.innerHTML = '<SPAN CLASS=\'' + PAGEITEMCLASS + '\'>';

					minPage = (CURRENTPAGE + 1) - 4;
					if (RPP > 0)
					{
						lastPage = Math.round((DATALENGTH/RPP) + 0.5) - 1;
					}
					else
					{
						lastPage = minPage;
					}

					if (minPage < 0)
					{
						minPage = 0;
					}
					maxPage = minPage + 6;
					if (maxPage > lastPage)
					{
						maxPage = lastPage;
					}


					if (lastPage > 0)
					{
						if (CURRENTPAGE > 0)
						{
							PGS.innerHTML += '<span class="' + PAGEITEMLINKCLASS + '" onmouseover="this.className=\'' + PAGEITEMHOVERCLASS + '\';" onmouseout="this.className=\'' + PAGEITEMLINKCLASS + '\';" onclick="Map_Fetch(' + TM + ',' + (CURRENTPAGE - 1) + ');">' + LOCALE_PAGEBACK + '</span>&nbsp;...&nbsp;';
							PGS.innerHTML += '<span class="' + PAGEITEMLINKCLASS + '" onmouseover="this.className=\'' + PAGEITEMHOVERCLASS + '\';" onmouseout="this.className=\'' + PAGEITEMLINKCLASS + '\';" onclick="Map_Fetch(' + TM + ',' + 0 + ');">' + LOCALE_PAGEFIRST + '</span>&nbsp;|&nbsp;';
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
								PGS.innerHTML += '<span class="' + PAGEITEMLINKCLASS + '" onmouseover="this.className=\'' + PAGEITEMHOVERCLASS + '\';" onmouseout="this.className=\'' + PAGEITEMLINKCLASS + '\';" onclick="Map_Fetch(' + TM + ',' + x + ');">' + (x+1) + '</span>';
							
							PGS.innerHTML += '&nbsp;';
						}
						if (CURRENTPAGE < lastPage)
						{
							PGS.innerHTML += '|&nbsp;<span class="' + PAGEITEMLINKCLASS + '" onmouseover="this.className=\'' + PAGEITEMHOVERCLASS + '\';" onmouseout="this.className=\'' + PAGEITEMLINKCLASS + '\';" onclick="Map_Fetch(' + TM + ',' + lastPage + ');">' + LOCALE_PAGELAST + '</span>&nbsp;...&nbsp;';
							PGS.innerHTML += '<span class="' + PAGEITEMLINKCLASS + '" onmouseover="this.className=\'' + PAGEITEMHOVERCLASS + '\';" onmouseout="this.className=\'' + PAGEITEMLINKCLASS + '\';" onclick="Map_Fetch(' + TM + ',' + (CURRENTPAGE + 1) + ');">' + LOCALE_PAGENEXT + '</span>';
						}
						else
						{
							PGS.innerHTML += '| ' + LOCALE_PAGELAST + ' ... ';
							PGS.innerHTML += '' + LOCALE_PAGENEXT + '';
						}
					}

					PGS.innerHTML += '</span>';
				}
			}
	}

	//FETCH THE ADDRESS LON/LAT BASED ON URL
	var AddressCallBack = false;
	function Map_Locate(ID,Address,CallBack)
	{
		if (CallBack)
		{
		    AddressCallBack = CallBack;
		}
		gWiz_FetchAddressStart(ID,Address);
	}
	
	//THE ADDRESS HAS BEEN FOUND, PARSE THE RETURN
	function Map_UseAddress(ID,latitude,longitude)
	{
		eval('STATUS' + ID).innerHTML = 'Loading Address'; //LOCALE_STATUS7;
	   Map_GoTo(ID,latitude,longitude);
	   if (AddressCallBack)
	   {
	      eval(AddressCallBack);
	   }
		eval('STATUS' + ID).innerHTML = '';
	}

	function Map_GoTo(ID,a,o)
	{
			eval('CURRENT' + ID + ' = 0;');
			eval('CURRENTPAGE' + ID + ' = 0;');
			eval('CLON' + ID + ' = o;');
			eval('CLAT' + ID + ' = a;');
			eval('CZOOM' + ID + ' = MAP' + ID + '.getZoomLevel();');
            
			Map_SetFocus(ID);
			eval('DATA' + ID + ' = false;');
			if (eval('DIRECTORY' + ID))
			{
				Map_Fetch(ID,0);
			}
			else
			{
				Map_Fetch(ID);
			}
	}

	function Map_Search(ID)
	{
		Map_Locate(ID,eval('SEARCHTXT' + ID + '.value'),null);
	}
	
	//]]>