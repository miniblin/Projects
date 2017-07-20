package com.example.fran.aseapp;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class MenuActivity extends AppCompatActivity {

    public static String postcode;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_menu);

        Button localMapButton = (Button)findViewById(R.id.localMapButton);
        localMapButton.setOnClickListener(localMapButtonListener);
        Button searchButton = (Button)findViewById(R.id.searchButton);
        searchButton.setOnClickListener(searchButtonListener);
    }

    private View.OnClickListener localMapButtonListener = new View.OnClickListener() {
        public void onClick(View v) {
            goToLocalMap(v);
        }

    };

    private View.OnClickListener searchButtonListener = new View.OnClickListener() {
        public void onClick(View v) {
            getPostCode(v);
        }
    };

    public void goToLocalMap(View view) {
        // create new Intent
        postcode ="";
        Intent newLocalMap = new Intent(this, MapsActivity.class);
        // adjust zoom / go to location
        // go to map
        startActivity(newLocalMap);
    }

    public void getPostCode(View view) {
        EditText mEdit = (EditText)findViewById(R.id.cityNameField);
        postcode = mEdit.getText().toString();
        Intent newLocalMap = new Intent(this, MapsActivity.class);
        startActivity(newLocalMap);
    }
}
