package com.example.fran.aseapp;

import android.app.Application;
import android.support.test.runner.AndroidJUnit4;
import android.test.ActivityInstrumentationTestCase2;
import android.test.ApplicationTestCase;
import android.test.suitebuilder.annotation.SmallTest;

import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;

/**
 * <a href="http://d.android.com/tools/testing/testing_android.html">Testing Fundamentals</a>
 */

@RunWith(AndroidJUnit4.class)
@SmallTest
public class ApplicationTest extends ActivityInstrumentationTestCase2<MapsActivity> {

    MapsActivity mapsActivity;


    public ApplicationTest() {
        super(MapsActivity.class);
    }

    @Override
    protected void setUp() throws Exception{
        System.out.println("Initializing MapsActivity.");
        super.setUp();

        mapsActivity = getActivity();
    }

    /*

    /*@Test
    public void testWifiChecker() throws Exception{

        Boolean expectedResult = true;

        Boolean actualResult = mapsActivity.checkWifi();

        assertTrue(actualResult == expectedResult);

    }*/


    @Test
    public void testContactServer() throws Exception{

    }

    @Test
    public void testOnClick() throws Exception{

    }

    @Test
    public void testStartAlarm() throws Exception{
        mapsActivity.startAlarm();
        assertTrue(mapsActivity.getManager() != null);
        assertTrue(mapsActivity.getPendingIntent() != null);
    }

    @Test
    public void testAddHeatMap() throws Exception{
        mapsActivity.addHeatMapPublic(50.123, -0.123);

        assertTrue(mapsActivity.getmOverlay() != null);
        assertTrue(mapsActivity.getmProvider() != null);
    }

    @Test
    public void testOnReceive() throws Exception{

    }

}