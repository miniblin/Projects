package com.example.fran.aseapp;


import android.support.test.espresso.ViewAssertion;
import android.support.test.espresso.assertion.ViewAssertions;
import android.support.test.rule.ActivityTestRule;
import android.support.test.runner.AndroidJUnit4;
import android.view.Menu;
import android.view.View;

import org.hamcrest.Matcher;
import org.junit.Rule;
import org.junit.Test;
import org.junit.runner.RunWith;

import static android.support.test.espresso.Espresso.onView;
import static android.support.test.espresso.action.ViewActions.click;
import static android.support.test.espresso.action.ViewActions.closeSoftKeyboard;
import static android.support.test.espresso.action.ViewActions.typeText;
import static android.support.test.espresso.assertion.ViewAssertions.matches;
import static android.support.test.espresso.matcher.ViewMatchers.isDisplayed;
import static android.support.test.espresso.matcher.ViewMatchers.withId;
import static android.support.test.espresso.matcher.ViewMatchers.withText;

/**
 * Created by Fran on 10/11/2016.
 */

@RunWith(AndroidJUnit4.class)
public class EspressoTest {

    @Rule
    public ActivityTestRule<MenuActivity> newMenuActivity = new ActivityTestRule<>(MenuActivity.class);

    @Test
    public void checkAppName() throws Exception {
        //check if name is displayed
        onView(withText("HeatmAPP")).check(matches(isDisplayed()));
    }

    @Test
    public void checkAppVersion() throws Exception {
        //check if name is displayed
        onView(withText("version 0.1")).check(matches(isDisplayed()));
    }

    @Test
    public void useCurrentLocation() {
        // check if 'use your location' button is displaying text and is clicked
        onView(withId(R.id.localMapButton)).check(matches(withText("Use your location")));
        onView(withId(R.id.localMapButton)).perform(click());
    }

    @Test
    public void changeText_newActivity() {
        // check if text is entered and search button clicked
        onView(withId(R.id.cityNameField)).perform(typeText("NewText"),
                closeSoftKeyboard());
        onView(withId(R.id.searchButton)).perform(click());
    }
}
