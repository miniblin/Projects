import java.net.*;
import java.io.*;

public class send {



    public static void main(String[] args) throws IOException{

	
	String location = "808080808";
	String userID = "222222222";
	String time = "2016-10-16 14:21:15";

        String urlString = "http://52.43.0.162/asegroup4/index.php?location=" + location + "&id=" + userID + "&time=" + time;

        URL url = new URL(urlString);
	
	/*
	String data = "fName=" + URLEncoder.encode("Atli", "UTF-8");
	HttpURLConnection connection = (HttpURLConnection) url.openConnection();
	
	try{
		connection.setDoInput(true);
		connection.setDoOutput(true);
		connection.setUseCaches(false);
		connection.setRequestMethod("POST");
		connection.setRequestProperty("Conetent-Type", "application/x-www-form-urlencoded");
	}finally{
		connection.disconnect();
	}
	*/
	
        String result = "";
        String data = "fName=" + URLEncoder.encode("Atli", "UTF-8");
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        try {
            connection.setDoInput(true);
            connection.setDoOutput(true);
            connection.setUseCaches(false);
            connection.setRequestMethod("POST");
            connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");

            DataInputStream in = new DataInputStream(connection.getInputStream());

            String g;
            while((g = in.readLine()) != null){
                result += g;
            }
            in.close();

        }finally {
            connection.disconnect();
            System.out.println(result);
        }
	


    }
}
