package com.example.fran.aseapp;

/**
 * Created by Dave on 16/10/2016.
 */


        import android.content.BroadcastReceiver;
        import android.content.Context;
        import android.content.Intent;
        import android.util.Log;
        import android.widget.Toast;

public class AlarmReceiver extends BroadcastReceiver {
    private double locationLong;
    private double locationLat;
    @Override
    public void onReceive(Context arg0, Intent arg1) {
        locationLat =  MapsActivity.mLastLocation.getLatitude();
        locationLong = MapsActivity.mLastLocation.getLongitude();

        // For our recurring task, we'll just display a message
        //Toast.makeText(arg0, "longitude: "+locationLong+" lattitude: "+ locationLat, Toast.LENGTH_SHORT).show();
        Log.d("Locations","Writing location to Toast");
        Intent sendLocation = new Intent(arg0, ContactServer.class);

        sendLocation.putExtra("locationLat",locationLat);
        sendLocation.putExtra("locationLong",locationLong);

       arg0.startService(sendLocation);

    }

}
