using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletMover : ManejoPallets {

    public MoveType miInput;
    public enum MoveType {
        WASD,
        Arrows
    }

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;
    [SerializeField] GameObject[] botones;

    private void Start()
    {
#if UNITY_EDITOR
        for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);
#elif UNITY_ANDROID || UNITY_IOS
        for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);
        if (botones[0] != null)
            botones[0].SetActive(true);
#endif

    }

    private void Update() {
        switch (miInput) {
            case MoveType.WASD:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.A)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.S)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.D)) {
                    TercerPaso();
                }
                break;
            case MoveType.Arrows:
                if (!Tenencia() && Desde.Tenencia() && Input.GetKeyDown(KeyCode.LeftArrow)) {
                    PrimerPaso();
                }
                if (Tenencia() && Input.GetKeyDown(KeyCode.DownArrow)) {
                    SegundoPaso();
                }
                if (segundoCompleto && Tenencia() && Input.GetKeyDown(KeyCode.RightArrow)) {
                    TercerPaso();
                }
                break;
            default:
                break;
        }
    }

    public void BotonBolsa(int boton)
    {
        if (!Tenencia() && Desde.Tenencia() && boton == 1)
        {
            PrimerPaso();
            botones[1].SetActive(true);
            segundoCompleto = false;
        }
        else if (Tenencia() && boton == 2)
        {
            SegundoPaso();
            botones[2].SetActive(true);
            segundoCompleto = true;
        }
        else if (segundoCompleto && Tenencia() && boton == 3)
        {
            TercerPaso();
            botones[1].SetActive(false);
            botones[2].SetActive(false);
            segundoCompleto = false;
        }
    }

    void PrimerPaso() {
        Desde.Dar(this);
        segundoCompleto = false;
    }
    void SegundoPaso() {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }
    void TercerPaso() {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor) {
        if (Tenencia()) {
            if (receptor.Recibir(Pallets[0])) {
                Pallets.RemoveAt(0);
            }
        }
    }
    public override bool Recibir(Pallet pallet) {
        if (!Tenencia()) {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        else
            return false;
    }
}
