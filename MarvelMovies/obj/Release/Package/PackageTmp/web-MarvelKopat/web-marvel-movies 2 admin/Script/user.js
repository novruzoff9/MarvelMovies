google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    var data = google.visualization.arrayToDataTable([
        ['Gun', 'Gundelik qeydiyyat', 'Umumi Say'],
        ['14.10.2021', 10, 10],
        ['15.10.2021', 12, 22],
        ['16.10.2021', 15, 37],
        ['17.10.2021', 20, 57]
    ]);

    var options = {
        width: 1000,
        height: 500,
        backgroundColor: '#282828',
        axisLabelsColor: '#ffffff',
        title: 'İstifadəçi sayı',
        //Basliq
        titleTextStyle: {
            color: "#ffffff",
            fontSize: 34
        },
        //X oxu
        hAxis: {
            textStyle: {
                fontSize: 18,
                color: "#ffffff",
                slant: '90deg'
            }
        },
        //Y oxu
        vAxis: {
            textStyle: {
                fontSize: 18,
                color: "#ffffff",
                slant: '90deg'
            }
        },
        curveType: 'function',
        colors: ['#244bf7', '#ee171f'],
        //Xett adlari
        legend: {
            position: 'bottom',
            textStyle: {
                fontSize: 18,
                color: '#ffffff'
            }
        },
        pointSize: 5,
        pointShape: 'circle',
        pointShadowColor: '#ffffff'
    };

    var chart = new google.visualization.LineChart(document.getElementById('user_chart'));

    chart.draw(data, options);
}