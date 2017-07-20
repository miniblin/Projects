/*
package com.example.fran.aseapp;


import android.app.Application;
import android.app.IntentService;
import android.content.Intent;
import android.icu.text.SimpleDateFormat;
import android.icu.util.Calendar;
import android.support.test.runner.AndroidJUnit4;
import android.test.ActivityInstrumentationTestCase2;
import android.test.ApplicationTestCase;
import android.test.suitebuilder.annotation.SmallTest;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
*/
/**
 * Created by Lucas on 04/11/2016.
 *
 * ContactServerTest class is a JUnit class that tests the functionality of the ContactServer
 * class.
 */
/*
@RunWith(AndroidJUnit4.class)
@SmallTest
public class ContactServerTest extends ActivityInstrumentationTestCase2<MapsActivity> {

    MapsActivity mapsActivity;
    IntentService contactServer;

    public ContactServerTest() {
        super(MapsActivity.class);
    }

    @Override
    protected void setUp() throws Exception {
        super.setUp();

        mapsActivity = getActivity();
        //contactServer = new IntentService(mapsActivity, ContactServer.class);

    }



    @Test
    public void contactServerCreationTest() {
        assertTrue(contactServer != null);
    }

    @Test
    public void onHandleIntentTest() {
        //test on handle intent
    }


    @Test
    public void serverConnectSuccessfulTest() {
        Calendar c = Calendar.getInstance();
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String strDate = sdf.format(c.getTime());

        int result_from_contactServer = contactServer.serverConnect(0,0,"test", strDate);
        System.out.println(result_from_contactServer);

        assertEquals(2, result_from_contactServer);
    }
}
*/