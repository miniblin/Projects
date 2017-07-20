package com.example.fran.aseapp;

import android.content.Context;
import android.provider.Settings;
import android.util.Log;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/**
 * Created by Dave on 24/10/2016.
 */

public class HeatMapData {

    private double latitude;
    private double longitude;
    private Context context;
    public static String[] latitudes;
    public static String[] longitudes;
    public static String[] values;
    public static String[] postCodes;


    public static final String KEY_LATITUDE = "Latitude";
    public static final String KEY_LONGITUDE = "Longitude";
    public static final String KEY_VALUE = "AVG(Value)";
    public static final String KEY_POSTCODE = "PostCode";

    private String json;

    public  String JSON_URL;



    public HeatMapData(String json){
        this.json = json;
        getHeatMapData();
    }

    public String[] getLatitudes(){
        return latitudes;
    }

    public String[] getLongitudes(){
        return longitudes;
    }

    public String[] getValues(){
        return values;
    }
    public String[] getPostCodes(){
        return postCodes;
    }
    //gets Data out of Json object
    public void getHeatMapData() {
        JSONArray jsonObject=null;
        try {
            jsonObject = new JSONArray(json);
            postCodes = new String[jsonObject.length()];
            latitudes = new String[jsonObject.length()];
            longitudes = new String[jsonObject.length()];
            values = new String[jsonObject.length()];

            //loops through Json xml and pulls out data into seperate arrays
            for(int i=0;i<jsonObject.length();i++){
                JSONObject jo = jsonObject.getJSONObject(i);
                postCodes[i] = jo.getString(KEY_POSTCODE);
                values[i] = jo.getString(KEY_VALUE);
                latitudes[i] = jo.getString(KEY_LATITUDE);
                longitudes[i] = jo.getString(KEY_LONGITUDE);
            }

            for(int i=0;i<latitudes.length;i++) {
                System.out.println(/*"Lat: "+latitudes[i]+"   Long: "+longitudes[i]+*/"PCOde: "+postCodes[i]+" Vals: "+values[i]);
            }
        }
        catch (JSONException e) {
            e.printStackTrace();
        }

    }
}
