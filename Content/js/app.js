const labels = [
    'Cevap '
];
const data = {
    labels: labels,
    datasets: [{
        label: 'Sonuçlar',
        backgroundColor: 'rgb(255, 99, 132)',
        borderColor: 'rgb(255, 99, 132)',
        data: [
            <operationBindings>
                <operationBinding operationType="fetch">
                    <customSQL>
                        from anketCevap in KullaniciAnketCevaps
                        join ac in AnketCevaps on anketCevap.CevapId equals ac.Id
                        where anketCevap.SoruId == 11
                        group ac by ac.Cevap into grouplanmis
                        select new {
                            CevapAdi = grouplanmis.Key,
                            Adet = (from t2 in grouplanmis select t2.Cevap).Count()
                            }
                    </customSQL>
                </operationBinding>
            </operationBindings>
        ],
    }]
};
const config = {
    type: 'line',
    data: data,
    options: {}
};
const myChart = new Chart(
    document.getElementById('myChart'),
    config
);

//from anketCevap in KullaniciAnketCevaps
//join ac in AnketCevaps on anketCevap.CevapId equals ac.Id
//where anketCevap.SoruId == 11
//group ac by ac.Cevap into grouplanmis
//select new {
//    CevapAdi =  grouplanmis.Key,
//    Adet = (from t2 in grouplanmis select t2.Cevap).Count()
//}