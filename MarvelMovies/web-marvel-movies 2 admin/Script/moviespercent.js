google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawPieChart);
function drawPieChart() {

    var data = google.visualization.arrayToDataTable([
        ['İtem', 'Sayı'],
        ['Film', 10],
        ['Serial', 8],
        ['Animasiya', 11]
    ]);

    var options = {
        width: 500,
        height: 500,
        backgroundColor: '#282828',
        title: 'Film nisbəti',
        //Basliq
        titleTextStyle: {
            color: "#ffffff",
            fontSize: 34
        },
        curveType: 'function',
        colors: ['#244bf7', '#ee171f', '#eeb71f'],
        //Xett adlari
        legend: {
            position: 'bottom',
            textStyle: {
                fontSize: 18,
                color: '#ffffff'
            }
        },
    };

    var chart = new google.visualization.PieChart(document.getElementById('moviespercent'));

    chart.draw(data, options);
}