    using System.Collections;
using System.Collections.Generic;
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine;
using UnityEngine.UI;

public class BancoSQLite : MonoBehaviour {

    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;

    public InputField ifLogin;
    public InputField ifSenha;

    public Text listaBD;

    private string senha;
    private string login;
    private string verifica;

    public Button botao;

    private string dbName = "URI=file:SQLiteDB.db";
    

    private void CreateAndInsertBD(){
        
        Debug.Log ("Conectando com o banco...");
        //using (var connection = new SqliteConnection (dbName)) {
        //connection.Open();

        //using (var command = connection.CreateCommand ()) {
        connection.CreateCommand();
        //connection.Open();
        command.CommandText = "CREATE TABLE IF NOT EXISTS usuario (id INTEGER PRIMARY KEY AUTOINCREMENT, login VARCHAR(30), senha VARCHAR(30));";
        command.ExecuteNonQuery();


        
        command.CommandText = "SELECT login FROM usuario WHERE login = 'teste';";
        reader = command.ExecuteReader();
        while(reader.Read()){
            verifica = reader.GetString(0);
        }
        
        if(verifica == "teste")
            Debug.Log("Usuario teste já existe.");
        else{
            command.CommandText = "INSERT INTO usuario (login, senha) VALUES('teste', 'teste');";
            command.ExecuteNonQuery();
            Debug.Log("Usuario teste inserido.");
        }    
    }

    public void verificaLogin() {
        Debug.Log ("Verificando login...");
        //connection.Open();
        if(string.IsNullOrEmpty(ifLogin.text))
            Debug.Log("Preencha o login!");
        else{
            command.CommandText = "SELECT login, senha FROM usuario WHERE login = '" + ifLogin.text + "';";
            reader = command.ExecuteReader();
            while(reader.Read()){
                login = reader.GetString(0);
                senha = reader.GetString(1);
                
                if (ifLogin.text.Equals(login) && ifSenha.text.Equals(senha))
                    Debug.Log ("Logado!");
                else
                    Debug.Log ("Login ou senha incorreto(s)!");
            }
        }
    }



    private void ConnectBD(){
        connection = new SqliteConnection(dbName);  
        command = connection.CreateCommand();
        connection.Open();
        
    }


    // Start is called before the first frame update
    void Start () {
        ConnectBD();
        CreateAndInsertBD();
    }

    // Update is called once per frame
    void Update () {

    }
}