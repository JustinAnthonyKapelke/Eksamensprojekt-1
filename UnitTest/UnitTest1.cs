using Microsoft.Data.SqlClient.DataClassification;
using Nordlangelands_Tækkemand.Model;
using Nordlangelands_Tækkemand.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UnitTest
{


    [TestClass]
    public class ThatchingTest
    {

        // CreateDelegate-metoden bruges som mock for ThatchingRepository.CreateMaterial i testen
        public ThatchingMaterial InitializeCreateDelegate(int materialID, string name, string description, int storageIndex, double price, int storageID)
        {
            return new ThatchingMaterial(materialID, name, description, storageIndex, price, storageID);
        }

        public ThatchingMaterial CreateDelegate(string name, string description, int storageIndex, double price, int storageID)
        {
            return new ThatchingMaterial(name, description, storageIndex, price, storageID);
        }

        ThatchingRepository thatchingRepo;
        ThatchingViewModel tvm;
        int MaterialID = 28;

        [TestMethod]
        public void CreateAndInsertMaterialTest()
        {
            // ARRANGE
            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
            tvm = new ThatchingViewModel();

            // ACT
            thatchingRepo.InitializeMaterials();

            tvm.CreateAndInsertMaterial("Frøgræs", "Langkornet frøgræs", 1, 80, 1);

            ThatchingMaterial createdMaterial = thatchingRepo.ReadMaterial(MaterialID);

            // ASSERT
            Assert.IsNotNull(createdMaterial);
            Assert.AreEqual("Frøgræs", createdMaterial.MaterialName);
            Assert.AreEqual("Langkornet frøgræs", createdMaterial.MaterialDescription);
            Assert.AreEqual(1, createdMaterial.MaterialStorageIndex);
            Assert.AreEqual(80, createdMaterial.MaterialPrice);
        }

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    //thatchingRepo.DeleteMaterial(ThatchingID);           
        //}

    }
}