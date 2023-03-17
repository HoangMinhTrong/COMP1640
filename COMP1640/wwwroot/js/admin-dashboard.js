const ideaTimelinesChart = document.getElementById('ideaTimelinesChart')
const barChartContributorDepartment = document.getElementById('barChartContributorDepartment');
const totalIdeaDepartmentsChart = document.getElementById('totalIdeaDepartmentsChart');
const percentIdeaDepartmentsChart = document.getElementById('percentIdeaDepartmentsChart');

async function getAnalysisData() {
    try {
        const response = await $.ajax({
            url: `${window.location.origin}/Analysis`,
            method: 'GET'
        });

        // Access the TotalIdeasTimelines field from the response
        var totalIdeasTimelines = response.totalIdeasTimelines;

        // Access the TotalIdeaDepartments field from the response
        var totalIdeaDepartments = response.totalIdeaDepartments;

        // Access the TotalContributorDepartments field from the response
        var totalContributorDepartments = response.totalContributorDepartments;

        fillTotalIdeaChart(totalIdeasTimelines)
        fillTotalIdeaDepartmentsChart(totalIdeaDepartments)
        fillBarChartContributorDepartment(totalContributorDepartments)
        
    } catch (error) {
        console.log(error);
    }
}

getAnalysisData();
function fillTotalIdeaChart(totalIdeasTimelines) {
    var totalIdeaslabels = [];
    var totalIdeasdataValues = [];
    $.each(totalIdeasTimelines, function (index, value) {
        totalIdeaslabels.push(value.label);
        totalIdeasdataValues.push(value.value);
    });

    new Chart(ideaTimelinesChart, {
        type: 'line',
        data: {
            labels: totalIdeaslabels,
            datasets: [{
                label: 'Idea Submited Over Time',
                data: totalIdeasdataValues,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}


function fillTotalIdeaDepartmentsChart(data) {
    var departments = [];
    var totalDepartmentIdeas = [];

    $.each(data, function (index, value) {
        departments.push(value.label);
        totalDepartmentIdeas.push(value.value);
    });
    new Chart(totalIdeaDepartmentsChart, {
        type: 'bar',
        data: {
            labels: departments,
            datasets: [{
                label: 'Total ideas',
                data: totalDepartmentIdeas,
                borderWidth: 1
            }]
        },
    });


    // Percentage
    var total = totalDepartmentIdeas.reduce(function(previousValue, currentValue) {
        return previousValue + currentValue;
    });
    var donutDataPercentages = totalDepartmentIdeas.map(function(value) {
        return Math.round(value / total * 100);
    });
    var options = {
        tooltips: {
            callbacks: {
                label: function (tooltipItem, data) {
                    var dataset = data.datasets[tooltipItem.datasetIndex];
                    var currentValue = dataset.data[tooltipItem.index];
                    return currentValue + "%";
                }
            }
        }
    };
    new Chart(percentIdeaDepartmentsChart, {
        type: 'doughnut',
        data: {
            labels: departments,
            datasets: [{
                label: '%',
                data: donutDataPercentages,
                borderWidth: 1
            }]
        },
        options: options
    });
}

function fillBarChartContributorDepartment(data) {
    var labels = [];
    var values = [];

    $.each(data, function (index, value) {
        labels.push(value.label);
        values.push(value.value);
    });
    new Chart(barChartContributorDepartment, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Total ideas',
                data: values,
                borderWidth: 1
            }]
        },
    });
}


    