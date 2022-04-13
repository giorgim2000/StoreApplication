using DXApplication1.Models;
using DXApplication1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXApplication1.BaseForms
{
    public partial class BaseDialog : Form
    {
        BaseForUpdateDto baseForUpdate = new BaseForUpdateDto();
        BaseForCreationDto baseToCreate;
        BaseService BaseService;
        CityService cityService;
        List<CityDto> cityList = new List<CityDto>();
        public BaseDialog(BaseDto baseDto)
        {
            InitializeComponent();            
            Text = "Edit Base";
            button1.Text = "Edit";
            label1.Text = "Edit Base";
            baseForUpdate.Id = baseDto.Id;
            baseNameTxtBox.Text = baseDto.BaseName;
            adressTxtBox.Text = baseDto.Adress;
            LoadCities(baseDto.CityName);
        }
        public BaseDialog()
        {
            InitializeComponent();
            LoadCities();
        }
        public async void LoadCities(string name = null)
        {
            cityService = new CityService();
            await cityService.GetData();
            if(CityService.Cities != null)
            {
                cityList = CityService.Cities;
            }

            foreach (var city in cityList)
            {
                cityComboBox.Items.Add(city.CityName);
            }

            if(name != null)
                cityComboBox.SelectedIndex = cityComboBox.Items.IndexOf(name);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if(cityComboBox.SelectedItem != null &&
                !string.IsNullOrEmpty(baseNameTxtBox.Text) &&
                !string.IsNullOrEmpty(adressTxtBox.Text))
            {
                BaseService = new BaseService();
                int cityId = cityList.Where(
                        i => i.CityName == cityComboBox.SelectedItem.ToString())
                        .FirstOrDefault()
                        .Id;
                if (button1.Text == "Edit")
                {
                    baseForUpdate.BaseName = baseNameTxtBox.Text;
                    baseForUpdate.Adress = adressTxtBox.Text;
                    await BaseService.UpdateBase(cityId, baseForUpdate);
                    this.Close();
                }
                else
                {
                    baseToCreate = new BaseForCreationDto()
                    {
                        BaseName = baseNameTxtBox.Text,
                        Adress = adressTxtBox.Text
                    };
                    

                    await BaseService.AddBase(cityId, baseToCreate);
                    this.Close();
                }
            }
        }
    }
}
