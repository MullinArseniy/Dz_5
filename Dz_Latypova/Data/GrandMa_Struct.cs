
namespace GrandMa_Hospital_Struct
{
    struct Grandma
    {
        public string Name;
        public int Age;
        public List<string> Diseases;
        public Dictionary<string, string> Medicines;

        public Grandma(string name, int age)
        {
            Name = name;
            Age = age;
            Diseases = new List<string>();
            Medicines = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            var diseases = Diseases.Count > 0 ? string.Join(", ", Diseases) : "нет болезней";
            return $"{Name}, {Age} лет, Болезни: {diseases}";
        }
    }

    struct Hospital
    {
        public string Name;
        public List<string> DiseasesTreated;
        public int Capacity;
        public Stack<Grandma> Patients;

        public Hospital(string name, List<string> diseasesTreated, int capacity)
        {
            Name = name;
            DiseasesTreated = diseasesTreated;
            Capacity = capacity;
            Patients = new Stack<Grandma>();
        }

        public double OccupancyPercent => (double)Patients.Count / Capacity * 100;

        public bool CanAdmit(Grandma grandma)
        {
            if (Patients.Count >= Capacity)
                return false;

            if (grandma.Diseases.Count == 0)
                return true;

            var treatedDiseases = DiseasesTreated; // Копируем в локальную переменную

            int treatableCount = grandma.Diseases.Count(d => treatedDiseases.Contains(d));

            return (double)treatableCount / grandma.Diseases.Count > 0.5;
        }

        public void Admit(Grandma grandma)
        {
            if (Patients.Count < Capacity)
                Patients.Push(grandma);
        }

        public override string ToString()
        {
            string diseases = DiseasesTreated.Count > 0 ? string.Join(", ", DiseasesTreated) : "нет болезней";
            return $"{Name}: лечит [{diseases}], пациентов: {Patients.Count}/{Capacity} ({OccupancyPercent:F2}%)";
        }
    }

}
