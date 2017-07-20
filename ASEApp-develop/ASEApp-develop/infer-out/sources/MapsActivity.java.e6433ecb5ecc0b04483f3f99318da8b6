package com.example.fran.aseapp;

import android.app.AlarmManager;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.PendingIntent;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.Rect;
import android.icu.text.SimpleDateFormat;
import android.icu.util.Calendar;
import android.net.Uri;
import android.support.annotation.RequiresApi;
import android.util.Log;
import android.view.View;
import android.widget.Toast;

import android.provider.Settings.Secure;

import android.Manifest;
import android.content.pm.PackageManager;
import android.location.Location;
import android.os.Build;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.FragmentActivity;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.widget.Toast;
import android.net.wifi.WifiManager;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.android.gms.appindexing.Action;
import com.google.android.gms.appindexing.AppIndex;
import com.google.android.gms.appindexing.Thing;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;

import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.Tile;
import com.google.android.gms.maps.model.TileOverlay;
import com.google.android.gms.maps.model.TileOverlayOptions;
import com.google.maps.android.heatmaps.Gradient;
import com.google.maps.android.heatmaps.HeatmapTileProvider;
import com.google.maps.android.heatmaps.WeightedLatLng;
import com.google.maps.android.ui.IconGenerator;

import org.json.JSONException;

import java.util.ArrayList;
import java.util.List;

public class MapsActivity extends FragmentActivity implements OnMapReadyCallback,
        GoogleMap.OnCameraChangeListener,
        GoogleApiClient.ConnectionCallbacks,
        GoogleApiClient.OnConnectionFailedListener,
        LocationListener {

    private GoogleMap mMap;
    private float zoomLevel;
    IconGenerator iconGenerator;
    GoogleApiClient mGoogleApiClient;
    static Location mLastLocation;
    Marker mCurrLocationMarker;
    LocationRequest mLocationRequest;

    WifiManager wifiManager;

    private HeatmapTileProvider mProvider;
    private TileOverlay mOverlay;


    private PendingIntent pendingIntent;
    private AlarmManager manager;
    /**
     * ATTENTION: This was auto-generated to implement the App Indexing API.
     * See https://g.co/AppIndexing/AndroidStudio for more information.
     */
    private GoogleApiClient client;
    public TileOverlay getmOverlay(){
        return mOverlay;
    }
    public HeatmapTileProvider getmProvider(){
        return mProvider;
    }
    public GoogleMap getmMap(){
        return mMap;
    }
    public AlarmManager getManager(){
        return manager;
    }
    public PendingIntent getPendingIntent(){
        return pendingIntent;
    }
    // checkWifi() checks if wifi is enabled

    public Boolean checkWifi(WifiManager wifiManager) {
        //WifiManager wifi = (WifiManager) getSystemService(Context.WIFI_SERVICE);
        if (wifiManager.isWifiEnabled()) {
            return true;
        }
        return false;
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_maps);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            checkLocationPermission();
        }
        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        mLastLocation = new Location("");
        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        client = new GoogleApiClient.Builder(this).addApi(AppIndex.API).build();
        wifiManager = (WifiManager) getSystemService(Context.WIFI_SERVICE);
    }


    @RequiresApi(api = Build.VERSION_CODES.N)
    @Override
    protected void onResume() {
        super.onResume(); // this line calls the super of onResume and doesn't crash

        if (!checkWifi(wifiManager)) {
            //Log.i("testing if wifi is on\n");
            //Toast.makeText(this, "Please Enable Wifi", Toast.LENGTH_LONG).show();
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.setTitle("Wifi not enabled");
            builder.setMessage("Enable Wifi?");
            builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    WifiManager wifi = (WifiManager) getSystemService(Context.WIFI_SERVICE);
                    wifi.setWifiEnabled(true);
                }
            });
            builder.setNegativeButton("Dismiss", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                    // dismiss
                }
            });
            AlertDialog dialog = builder.create();
            dialog.show();
        }



        Calendar c = Calendar.getInstance();
        SimpleDateFormat sdf = new SimpleDateFormat("dd/mm/yyyy hh:mm:ss");
        String strDate = sdf.format(c.getTime());
        //Toast.makeText(this, strDate, Toast.LENGTH_SHORT).show();

    }

    /**
     * Manipulates the map once available.
     * This callback is triggered when the map is ready to be used.
     * This is where we can add markers or lines, add listeners or move the camera. In this case,
     * we just add a marker near Sydney, Australia.
     * If Google Play services is not installed on the device, the user will be prompted to install
     * it inside the SupportMapFragment. This method will only be triggered once the user has
     * installed Google Play services and returned to the app.
     */
    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;
        mMap.setMapType(GoogleMap.MAP_TYPE_HYBRID);

        //Initialize Google Play Services
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            if (ContextCompat.checkSelfPermission(this,
                    Manifest.permission.ACCESS_FINE_LOCATION)
                    == PackageManager.PERMISSION_GRANTED) {
                buildGoogleApiClient();
                mMap.setMyLocationEnabled(true);
            }
        } else {
            buildGoogleApiClient();
            mMap.setMyLocationEnabled(true);
        }

        iconGenerator = new IconGenerator(getApplicationContext());

        GoogleMap.OnCameraChangeListener cameraChangeListener = new GoogleMap.OnCameraChangeListener() {
            @Override
            public void onCameraChange(CameraPosition cameraPosition) {
                zoomLevel = mMap.getCameraPosition().zoom;
                System.out.println("CURRENT ZOOM LEVEL = " + zoomLevel);
                if(zoomLevel > 17) {
                    System.out.println("zoomed way the fuck in mate");
                }else if (zoomLevel < 10){
                    System.out.println("showing citiesW");
                }
            }
        };
        mMap.setOnCameraChangeListener(cameraChangeListener);
        startAlarm();


    }


    protected synchronized void buildGoogleApiClient() {
        mGoogleApiClient = new GoogleApiClient.Builder(this)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API)
                .build();
        mGoogleApiClient.connect();
    }

    @Override
    public void onConnected(Bundle bundle) {
        //Toast.makeText(this, "This is a test toast, no idea where it will show up", Toast.LENGTH_LONG).show();
        mLocationRequest = new LocationRequest();
        mLocationRequest.setInterval(1000);
        mLocationRequest.setFastestInterval(1000);
        mLocationRequest.setPriority(LocationRequest.PRIORITY_BALANCED_POWER_ACCURACY);
        if (ContextCompat.checkSelfPermission(this,
                Manifest.permission.ACCESS_FINE_LOCATION)
                == PackageManager.PERMISSION_GRANTED) {
            LocationServices.FusedLocationApi.requestLocationUpdates(mGoogleApiClient, mLocationRequest, this);
        }

    }

    @Override
    public void onConnectionSuspended(int i) {
        Toast.makeText(this, "connection suspended", Toast.LENGTH_LONG).show();
    }

    @Override
    public void onLocationChanged(Location location) {

        mLastLocation = location;
        addHeatMap(mLastLocation.getLatitude(), mLastLocation.getLongitude());
        // get postcode
        // MenuActivity.postcode

        //addHeatMap(51.8227, -0.1398);
        if (mCurrLocationMarker != null) {
            mCurrLocationMarker.remove();
        }

        //Place current location marker
        LatLng latLng = new LatLng(location.getLatitude(), location.getLongitude());
        MarkerOptions markerOptions = new MarkerOptions();
        markerOptions.position(latLng);
        markerOptions.title("Current Position");
        markerOptions.icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_MAGENTA));
        //mCurrLocationMarker = mMap.addMarker(markerOptions);

        //move map camera
        mMap.moveCamera(CameraUpdateFactory.newLatLng(latLng));
        mMap.animateCamera(CameraUpdateFactory.zoomTo(11));

        //stop location updates
        if (mGoogleApiClient != null) {
            LocationServices.FusedLocationApi.removeLocationUpdates(mGoogleApiClient, this);
        }

    }


    @Override
    public void onConnectionFailed(ConnectionResult connectionResult) {
        Toast.makeText(this, "connection failed", Toast.LENGTH_LONG).show();
    }

    public static final int MY_PERMISSIONS_REQUEST_LOCATION = 99;

    public boolean checkLocationPermission() {
        if (ContextCompat.checkSelfPermission(this,
                Manifest.permission.ACCESS_FINE_LOCATION)
                != PackageManager.PERMISSION_GRANTED) {

            // Asking user if explanation is needed
            if (ActivityCompat.shouldShowRequestPermissionRationale(this,
                    Manifest.permission.ACCESS_FINE_LOCATION)) {

                // Show an explanation to the user *asynchronously* -- don't block
                // this thread waiting for the user's response! After the user
                // sees the explanation, try again to request the permission.

                //Prompt the user once explanation has been shown
                ActivityCompat.requestPermissions(this,
                        new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                        MY_PERMISSIONS_REQUEST_LOCATION);


            } else {
                // No explanation needed, we can request the permission.
                ActivityCompat.requestPermissions(this,
                        new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                        MY_PERMISSIONS_REQUEST_LOCATION);
            }
            return false;
        } else {
            return true;
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode,
                                           String permissions[], int[] grantResults) {
        switch (requestCode) {
            case MY_PERMISSIONS_REQUEST_LOCATION: {
                // If request is cancelled, the result arrays are empty.
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {

                    // permission was granted. Do the
                    // contacts-related task you need to do.
                    if (ContextCompat.checkSelfPermission(this,
                            Manifest.permission.ACCESS_FINE_LOCATION)
                            == PackageManager.PERMISSION_GRANTED) {

                        if (mGoogleApiClient == null) {
                            buildGoogleApiClient();
                        }
                        mMap.setMyLocationEnabled(true);
                    }

                } else {

                    // Permission denied, Disable the functionality that depends on this permission.
                    Toast.makeText(this, "permission denied", Toast.LENGTH_LONG).show();
                }
                return;
            }

            // other 'case' lines to check for other permissions this app might request.
            // You can add here other case statements according to your requirement.
        }
    }

    public void startAlarm() {
        manager = (AlarmManager) getSystemService(Context.ALARM_SERVICE);
        int interval = 30000;
        int passing = 1;

        Intent alarmIntent = new Intent(this, AlarmReceiver.class);
        alarmIntent.putExtra("location", Integer.toString(passing++));
        pendingIntent = PendingIntent.getBroadcast(this, 0, alarmIntent, 0);

        manager.setRepeating(AlarmManager.RTC_WAKEUP, System.currentTimeMillis(), interval, pendingIntent);
    }


    private void addHeatMapTestable(double lat, double lon, String mockJson){
        double latitude = lat;
        final double longitude = lon;
        String JSON_URL = "http://users.sussex.ac.uk/~dil20/heatmap.php?latitude=" + latitude + "&longitude=" + longitude;
        StringRequest stringRequest = new StringRequest(JSON_URL,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        System.out.println("addHeatMap called");
                        Log.d("heat", response);
                        HeatMapData heatmap;
                        String json = response;
                        heatmap = new HeatMapData(json);


                        Log.d("heat", "adding heatmap");
                        List<WeightedLatLng> list = null;

                        list = new ArrayList<WeightedLatLng>();
                        String[] latitudes = heatmap.getLatitudes();
                        String[] longitudes = heatmap.getLongitudes();
                        String[] values = heatmap.getValues();
                        String[] postCodes = heatmap.getPostCodes();
                        for (int i = 0; i < latitudes.length; i++) {
                            double lat = Double.parseDouble(latitudes[i]);
                            double lng = Double.parseDouble(longitudes[i]);
                            double val = Double.parseDouble(values[i]);
                            System.out.println("PostCodes: " + postCodes[i] + " val: " + val);
                            LatLng latLong = new LatLng(lat, lng);

                            list.add(new WeightedLatLng(latLong, i));
                        }

                        int[] colors = {

                                Color.rgb(102, 225, 0),// green
                                Color.rgb(255, 0, 0)// red
                        };
                        int[] oldColors = {
                                Color.rgb(0, 0, 255), Color.rgb(255, 0, 255)
                        };
                        float[] startPoints = {
                                0.01f, 1f
                        };
                        /*
                            if(mode == old prices){
                                Gradient = new Gradient(oldColors, startPoints);
                            }else{
                                Gradient = new Gradient(colors, startPoints);

                         */
                        //Gradient gradient = new Gradient(colors, startPoints);
                        Gradient gradient = new Gradient(oldColors, startPoints);

                        mProvider = new HeatmapTileProvider.Builder()
                                .weightedData(list)
                                .gradient(gradient)
                                .radius(30)
                                .build();
                        mOverlay = mMap.addTileOverlay(new TileOverlayOptions().tileProvider(mProvider));


                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        //error message
                    }
                });
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);


    }

    public void addHeatMapPublic(double lat, double lon){
        addHeatMap(lat, lon);
    }
    //creates heatmap in conjunction with heatmap data class
    private void addHeatMap(double lat, double lon) {

        double latitude = lat;
        final double longitude = lon;
        String JSON_URL = "http://users.sussex.ac.uk/~dil20/heatmap.php?latitude=" + latitude + "&longitude=" + longitude;
        StringRequest stringRequest = new StringRequest(JSON_URL,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        System.out.println("addHeatMap called");
                        Log.d("heat", response);
                        HeatMapData heatmap;
                        String json = response;
                        heatmap = new HeatMapData(json);


                        Log.d("heat", "adding heatmap");
                        List<WeightedLatLng> list = null;

                        list = new ArrayList<WeightedLatLng>();
                        String[] latitudes = heatmap.getLatitudes();
                        String[] longitudes = heatmap.getLongitudes();
                        String[] values = heatmap.getValues();
                        String[] postCodes = heatmap.getPostCodes();
                        for (int i = 0; i < latitudes.length; i++) {
                            double lat = Double.parseDouble(latitudes[i]);
                            double lng = Double.parseDouble(longitudes[i]);
                            double val = Double.parseDouble(values[i]);
                            System.out.println("PostCodes: " + postCodes[i] + " val: " + val);
                            LatLng latLong = new LatLng(lat, lng);

                            list.add(new WeightedLatLng(latLong, i));
                        }

                        int[] colors = {

                                Color.rgb(102, 225, 0),// green
                                Color.rgb(255, 0, 0)// red
                        };
                        int[] oldColors = {
                                Color.rgb(0, 0, 255), Color.rgb(255, 0, 255)
                        };
                        float[] startPoints = {
                                0.01f, 1f
                        };
                        /*
                            if(mode == old prices){
                                Gradient = new Gradient(oldColors, startPoints);
                            }else{
                                Gradient = new Gradient(colors, startPoints);

                         */
                        
                        Gradient gradient = new Gradient(colors, startPoints);
                        //Gradient gradient = new Gradient(oldColors, startPoints);

                        mProvider = new HeatmapTileProvider.Builder()
                                .weightedData(list)
                                .gradient(gradient)
                                .radius(30)
                                .build();
                        mOverlay = mMap.addTileOverlay(new TileOverlayOptions().tileProvider(mProvider));


                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        //error message
                    }
                });
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);


    }

    @Override
    public void onCameraChange(CameraPosition cameraPosition) {
        System.out.println("camera changed");
    }

    /**
     * ATTENTION: This was auto-generated to implement the App Indexing API.
     * See https://g.co/AppIndexing/AndroidStudio for more information.
     */
    public Action getIndexApiAction() {
        Thing object = new Thing.Builder()
                .setName("Maps Page") // TODO: Define a title for the content shown.
                // TODO: Make sure this auto-generated URL is correct.
                .setUrl(Uri.parse("http://[ENTER-YOUR-URL-HERE]"))
                .build();
        return new Action.Builder(Action.TYPE_VIEW)
                .setObject(object)
                .setActionStatus(Action.STATUS_TYPE_COMPLETED)
                .build();
    }

    @Override
    public void onStart() {
        super.onStart();

        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        client.connect();
        AppIndex.AppIndexApi.start(client, getIndexApiAction());
    }

    @Override
    public void onStop() {
        super.onStop();

        // ATTENTION: This was auto-generated to implement the App Indexing API.
        // See https://g.co/AppIndexing/AndroidStudio for more information.
        AppIndex.AppIndexApi.end(client, getIndexApiAction());
        client.disconnect();
    }
}

