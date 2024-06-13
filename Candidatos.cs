using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Trabalho_AED
{
    internal class Candidatos
    {
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private double notaRedacao;
        public double NotaRedacao
        {
            get { return notaRedacao; }
            set { notaRedacao = value; }
        }

        private double notaMat;
        public double NotaMat
        {
            get { return notaMat; }
            set { notaMat = value; }
        }

        private double notaLing;
        public double NotaLing
        {
            get { return notaLing; }
            set { notaLing = value; }
        }

        public int codCursoOp1;
        public int codCursoOp2;

        public bool aprovadoPrimeira;

        private double media;
        public double Media
        {
            get { return media; }
            set { media = value; }
        }
        public Candidatos(string nome, int notaRedacao, int notaMat, int notaLing, int codCursoOp1, int codCursoOp2)
        {
            this.nome = nome;
            this.notaRedacao = notaRedacao;
            this.notaMat = notaMat;
            this.notaLing= notaLing;
            this.codCursoOp1 = codCursoOp1;
            this.codCursoOp2 = codCursoOp2;
            this.media = (notaRedacao + notaMat + notaLing) / 3;
            this.aprovadoPrimeira = false;
        }

    }
}
