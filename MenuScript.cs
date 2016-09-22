using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{

	//Variaveis globais de todo programa:
	public int velocidade;
	public int altura;
	public int distancia;
	public int angulo;

	//Variáveis locais do menu:
	public Text textAltura;
	public Text textDistancia;
	public Text textAngulo;
	public Button comecar;
    public Button opcoes;
    public Button sair;
    public Button voltar;
	public InputField vetorVelocidade;
	public InputField entradaAltura;
	public InputField entradaDistancia;
	public InputField entradaAngulo;
	public Dropdown estiloVisao;
	public Canvas menuOpcoes;
    public Canvas menuInicial;

	// MODIFICAÇÃO
	private string entradaDistanciaTemporaria;
	private string entradaAnguloTemporaria;

	// MODIFICAÇÂO
	void startConfig(){
		string[] lines = { "CONFIG FILE - DO NOT MODIFY", "ARQUIVO DE CONFIGURAÇÃO - NÃO MODIFICAR", "0", "0", "0", "0", "0" };
		System.IO.File.WriteAllLines (@"ConfigUNITY.txt", lines);
	}

	// MODIFICAÇÃO
	void writeAllOnConfig(string text){
		print ("CALLED");
		print (text);
		string[] lines = readFromConfig ();

		if(text == "Desativado"){
			return;
		}
		if (text == "2") {
			lines [2] = vetorVelocidade.text;
		}
		if (text == "3"){
			lines [3] = estiloVisao.value.ToString ();
		}
		if (text == "4"){
			lines [4] = entradaAltura.text;
		}
		if (text == "5"){
			lines [5] = entradaDistancia.text;
		}
		if (text == "6"){
			lines [6] = entradaAngulo.text;
		}
		if (text == "All") {
			lines [2] = vetorVelocidade.text;
			lines [3] = estiloVisao.value.ToString ();
			lines [4] = entradaAltura.text;
			lines [5] = entradaDistancia.text;
			lines [6] = entradaAngulo.text;
		}
		System.IO.File.WriteAllLines (@"ConfigUNITY.txt", lines);
	}

	// MODIFICAÇÃO
	void setFromConfig(){
		string[] lines = System.IO.File.ReadAllLines (@"ConfigUNITY.txt");
		/*
		for (int i = 0; i < lines.Length; i++) {
			print (lines [i]);
		}
		*/
		vetorVelocidade.text = lines[2];
		estiloVisao.value = Int32.Parse(lines[3]);
		entradaAltura.text = lines [4];
		entradaDistancia.text = lines [5];
		entradaAngulo.text = lines [6];

		if(estiloVisao.value == 0){
			entradaAngulo.text = "Desativado";
			entradaDistancia.text = "Desativado";
		}
	}

	string[] readFromConfig(){
		string[] lines = System.IO.File.ReadAllLines (@"ConfigUNITY.txt");
		return lines;
	}


    // Use this for initialization
    void Start()
    {
		startConfig ();
        menuInicial = menuInicial.GetComponent<Canvas>();
        menuInicial.enabled = true;
        menuOpcoes = menuOpcoes.GetComponent<Canvas>();
        menuOpcoes.enabled = false;
        voltar = voltar.GetComponent<Button>();
        comecar = comecar.GetComponent<Button>();
        opcoes = opcoes.GetComponent<Button>();
		sair = sair.GetComponent<Button>();
        voltar.enabled = false;

		//Inicializa as opções
		//Texts
		textAltura = textAltura.GetComponent<Text> ();
		textAltura.enabled = false;
		textDistancia = textDistancia.GetComponent<Text> ();
		textDistancia.enabled = false;
		textAngulo = textAngulo.GetComponent<Text> ();
		textAngulo.enabled = false;

		//Input Fields
		entradaAltura = entradaAltura.GetComponent<InputField>();
		entradaAltura.enabled = false;
		entradaAltura.text = "0";

		entradaDistancia = entradaDistancia.GetComponent<InputField>();
		entradaDistancia.enabled = false;
		entradaDistancia.text = "0";

		entradaAngulo = entradaAngulo.GetComponent<InputField>();
		entradaAngulo.enabled = false;
		entradaAngulo.text = "0";

		vetorVelocidade = vetorVelocidade.GetComponent<InputField>();
		vetorVelocidade.enabled = false;
		vetorVelocidade.text = "0";

		estiloVisao = estiloVisao.GetComponent<Dropdown>();
		estiloVisao.enabled = false;
		estiloVisao.value = 0;

		//Aqui deve recuperar as preferencias do usuário.
        

    }

    public void Opcoes()
    {
        menuInicial.enabled = false;
        menuOpcoes.enabled = true;
        voltar.enabled = true;
        comecar.enabled = false;
        opcoes.enabled = false;
		sair.enabled = false;
		vetorVelocidade.enabled = true;
		estiloVisao.enabled = true;
		textAltura.enabled = true;
		entradaAltura.enabled = true;
		entradaAngulo.enabled = false;
		entradaDistancia.enabled = false;
		changedEstiloVisao ();

		// MODIFICAÇÃO
		setFromConfig();

		// MODIFICAÇÃO
		vetorVelocidade.onValueChanged.AddListener(delegate {writeAllOnConfig(check(vetorVelocidade.text, "2"));});
		estiloVisao.onValueChanged.AddListener(delegate {changedEstiloVisao();});
		entradaAltura.onValueChanged.AddListener(delegate {writeAllOnConfig(check(entradaAltura.text, "4"));});
		entradaDistancia.onValueChanged.AddListener(delegate {writeAllOnConfig(check(entradaDistancia.text, "5"));});
		entradaAngulo.onValueChanged.AddListener(delegate {writeAllOnConfig(check(entradaAngulo.text, "6"));});

    }

	private string check(string text, string option){
		if(text == "Desativado"){
			return "Desativado";
		}
		return option;
	}

	public void changedEstiloVisao()
	{
		writeAllOnConfig ("3");
		//Caso seja visão vertical, só é necessário mexer na altura.
		if (estiloVisao.value == 1) {

			// MODIFICAÇÃO
			//if (entradaAnguloTemporaria != "Desativado") {
			if(textAngulo.text != "Desativado"){
				//textAngulo.text = entradaAnguloTemporaria;
				//entradaAngulo.text = entradaAnguloTemporaria;
				setFromConfig();
			} else {
				//textAngulo.text = "0";
				entradaAngulo.text = "0";
			}

			// MODIFICAÇÃO
			//if(entradaDistanciaTemporaria != "Desativado"){
			if(textDistancia.text != "Desativado"){
				//textDistancia.text = entradaDistanciaTemporaria;
				//entradaDistancia.text = entradaDistanciaTemporaria;
				setFromConfig();
			} else {
				//textDistancia.text = "0";
				entradaDistancia.text = "0";
			}

			textAngulo.enabled = true;
			textDistancia.enabled = true;
			entradaAngulo.enabled = true;
			entradaDistancia.enabled = true;
		}
		//Caso seja visão oblíqua, necessário mexer também na opção angulo e de distância
		else if(estiloVisao.value == 0) {

			// MODIFICAÇÃO
			//entradaAnguloTemporaria = textAngulo.text;
			//entradaDistanciaTemporaria = textDistancia.text;
			//textAngulo.text = "Desativado";
			//textDistancia.text = "Desativado";

			//entradaAnguloTemporaria = entradaAngulo.text;
			//entradaDistanciaTemporaria = entradaDistancia.text;
			entradaAngulo.text = "Desativado";
			entradaDistancia.text = "Desativado";

			textAngulo.enabled = true;
			textDistancia.enabled = true;
			entradaAngulo.enabled = false;
			entradaDistancia.enabled = false;
		}
	}


	public void resetOptions()
	{
		if (!vetorVelocidade.text.Equals ("")) { 
			velocidade = int.Parse (vetorVelocidade.text);
		}
		else if(!entradaAltura.text.Equals("")) {
			altura = int.Parse (entradaAltura.text);
		}
		else if(!entradaAltura.text.Equals("")) {
			distancia = int.Parse (entradaDistancia.text);
		}
		else if(!entradaAltura.text.Equals("")) {
			angulo = int.Parse (entradaAngulo.text);
		}
		vetorVelocidade.text = "";
		//Debug.Log(velocidade);

	}

    public void Voltar() {
		resetOptions();
        menuInicial.enabled = true;
        menuOpcoes.enabled =  false;
        voltar.enabled = false;
        comecar.enabled = true;
        opcoes.enabled = true;
        sair.enabled = true;
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Comecar()
    {
        Application.LoadLevel("KinectOverlayDemo");
    }

}